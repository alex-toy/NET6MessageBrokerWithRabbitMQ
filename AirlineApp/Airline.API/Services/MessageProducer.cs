using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace Airline.API.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void Send<T>(T message)
        {
            using IModel channel = GetChannel();

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "booking", body: body);
        }

        private static IModel GetChannel()
        {
            var factory = new ConnectionFactory()
            {
                //HostName = "localhost",
                //UserName = "user",
                //Password = "password",
                //VirtualHost = "/",

                Uri = new Uri("amqp://user:password@localhost:5672")
            };

            var connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.QueueDeclare("booking", durable: true, exclusive: true);
            return channel;
        }
    }
}
