using MessageQueueHandler;

Console.WriteLine("Rabbit Sender");

var messageQueueSender = new MessageQueueSender("demoExchange", "demo-routing-key", "demoQueue");

messageQueueSender.Send($"Message sent by server");

//var messageQueueSender = new MessageQueueSender("demoExchange", "demo-routing-key", "demoQueue");

//for (int i = 0; i < 100; i++)
//{
//    messageQueueSender.Send($"hello friends - {i}");
//    Thread.Sleep(1000);
//}



messageQueueSender.Close();