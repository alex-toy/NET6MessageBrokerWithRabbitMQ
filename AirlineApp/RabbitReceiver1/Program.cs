// See https://aka.ms/new-console-template for more information
using MessageQueueHandler;

Console.WriteLine("RabbitMQ receiver 1");

var messageQueueReceiver = new MessageQueueReceiver("demoExchange", "demo-routing-key", "demoQueue");

string consumerTag = messageQueueReceiver.Consume();

messageQueueReceiver.Close(consumerTag);
Console.ReadLine();