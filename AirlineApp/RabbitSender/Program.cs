// See https://aka.ms/new-console-template for more information
using MessageQueueHandler;

Console.WriteLine("Rabbit Sender");

var messageQueueSender = new MessageQueueSender("demoExchange", "demo-routing-key", "demoQueue");
messageQueueSender.Send("hello friends");
messageQueueSender.Close();