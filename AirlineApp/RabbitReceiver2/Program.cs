using MessageQueueHandler;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("RabbitMQ receiver 2");

EventHandler<BasicDeliverEventArgs> messageReceivedEventHandler = (sender, args) =>
{
    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
    byte[] body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"New message : {message} processed by receiver 2");
};

var messageQueueReceiver = new MessageQueueReceiver("demoExchange", "demo-routing-key", "demoQueue", messageReceivedEventHandler);

string consumerTag = messageQueueReceiver.Consume();

Console.ReadLine();
messageQueueReceiver.Close(consumerTag);