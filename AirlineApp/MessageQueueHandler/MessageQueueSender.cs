using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MessageQueueHandler
{
    public class MessageQueueSender
    {
        public readonly string _exchangeName;
        public readonly string _routingKey;
        public readonly string _queueName;

        private IModel _channel { get; set; }
        private IConnection _connection { get; set; }

        public MessageQueueSender(string exchangeName, string routingKey, string queueName)
        {
            _exchangeName = exchangeName;
            _routingKey = routingKey;
            _queueName = queueName;

            SetChannel();
        }

        public void Send<T>(T message)
        {
            string jsonString = JsonSerializer.Serialize(message);
            byte[] body = Encoding.UTF8.GetBytes(jsonString);

            _channel.BasicPublish(_exchangeName, _routingKey, body: body);
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }

        private void SetChannel()
        {
            var factory = new ConnectionFactory()
            {
                ClientProvidedName = "Rabbit sender app",
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(_queueName, durable: false, exclusive: false, false);
            _channel.QueueBind(_queueName, _exchangeName, _routingKey, null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, false);
        }
    }
}