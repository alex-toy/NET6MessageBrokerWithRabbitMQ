using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MessageQueueHandler
{
    public class MessageQueueSender
    {
        public readonly string _exchangeName = "demoExchange";
        public readonly string _routingKey = "demo-routing-key";
        public readonly string _queueName = "demoQueue";

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
            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

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
                Uri = new Uri("amqp://user:password@localhost:5672")
            };

            _connection = factory.CreateConnection();
            IModel channel = _connection.CreateModel();

            channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(_queueName, durable: false, exclusive: false);
            channel.QueueBind(_queueName, _exchangeName, _routingKey);

            _channel = channel;
        }
    }
}