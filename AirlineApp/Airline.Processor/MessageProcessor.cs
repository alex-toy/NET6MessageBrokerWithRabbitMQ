using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Airline.Processor
{
    public class MessageProcessor
    {
        private IModel _channel { get; set; }
        private EventingBasicConsumer _consumer { get; set; }

        public MessageProcessor()
        {
            _channel = GetChannel();
            SetConsumer();
        }

        public void Consume()
        {
            _channel.BasicConsume("bookings", true, _consumer);
        }

        private void SetConsumer()
        {
            _consumer = GetConsumer();

            _consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"new message : {message}");
            };
        }

        private EventingBasicConsumer GetConsumer()
        {
            _channel.QueueDeclare("booking", durable: true, exclusive: true);
            return new EventingBasicConsumer(_channel);
        }

        private IModel GetChannel()
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
            return channel;
        }
    }
}
