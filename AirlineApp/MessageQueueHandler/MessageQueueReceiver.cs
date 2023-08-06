using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageQueueHandler
{
    public class MessageQueueReceiver
    {
        public readonly string _exchangeName;
        public readonly string _routingKey;
        public readonly string _queueName;

        private IModel _channel { get; set; }
        private EventingBasicConsumer _consumer { get; set; }
        private EventHandler<BasicDeliverEventArgs> _eventHandler { get; set; }

        public MessageQueueReceiver(string routingKey, string queueName, string exchangeName, EventHandler<BasicDeliverEventArgs> eventHandler)
        {
            _exchangeName = exchangeName;
            _routingKey = routingKey;
            _queueName = queueName;
            _eventHandler = eventHandler;

            SetChannel();
            SetConsumer();
        }

        public string Consume()
        {
            string consumerTag = _channel.BasicConsume(_queueName, false, _consumer);
            return consumerTag;
        }

        public void Close(string consumerTag)
        {
            _channel.BasicCancel(consumerTag);
            _channel.Close();
        }

        private void SetConsumer()
        {
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += _eventHandler;
        }

        private void SetChannel()
        {
            var factory = new ConnectionFactory()
            {
                ClientProvidedName = "Rabbit sender app",
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(_queueName, durable: false, exclusive: false);
            _channel.QueueBind(_queueName, _exchangeName, _routingKey);
            _channel.BasicQos(prefetchSize:0, prefetchCount:1, false); ;
        }
    }
}
