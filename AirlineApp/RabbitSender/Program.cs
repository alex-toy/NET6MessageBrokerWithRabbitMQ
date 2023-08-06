// See https://aka.ms/new-console-template for more information
using MessageQueueHandler;

Console.WriteLine("Rabbit Sender");

var messageQueueSender = new MessageQueueSender("demoExchange", "demo-routing-key", "demoQueue");

for (int i = 0; i < 10; i++)
{
    messageQueueSender.Send($"hello friends - {i}");
}
messageQueueSender.Close();