using MessageQueueHandler;

Console.WriteLine("Rabbit Sender");

var messageQueueSender = new MessageQueueSender("demoExchange", "demo-routing-key", "demoQueue");

for (int i = 0; i < 100; i++)
{
    messageQueueSender.Send($"Message number {i}");
    Thread.Sleep(1000);
}



messageQueueSender.Close();