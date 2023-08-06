using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Airline.Processor
{
    public class MessageProcessor
    {
        public readonly string _exchangeName = "demoExchange";
        public readonly string _routingKey = "demo-routing-key";
        public readonly string _queueName = "booking";

        private IModel _channel { get; set; }
        private EventingBasicConsumer _consumer { get; set; }

        public MessageProcessor(string routingKey, string queueName, string exchangeName)
        {
            _exchangeName = exchangeName;
            _routingKey = routingKey;
            _queueName = queueName;

            SetChannel();
            SetConsumer();
        }

        public void Consume()
        {
            _channel.BasicConsume("bookings", true, _consumer);
        }

        private void SetConsumer()
        {
            _consumer = new EventingBasicConsumer(_channel);

            _consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"new message : {message}");
            };
        }

        private void SetChannel()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                UserName = "user",
                Password = "password",
                VirtualHost = "vhost",
                //Uri = "amqp://user:pass@hostName:port/vhost"
            };

            var connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            _channel.QueueDeclare(_queueName, durable: true, exclusive: true);

            _channel = channel;
        }
    }
}
