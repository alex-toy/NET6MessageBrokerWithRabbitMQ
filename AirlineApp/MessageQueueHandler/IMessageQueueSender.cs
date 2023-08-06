namespace MessageQueueHandler
{
    public interface IMessageQueueSender
    {
        void Close();
        void Send<T>(T message);
    }
}