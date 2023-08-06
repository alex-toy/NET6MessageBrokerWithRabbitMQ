namespace Airline.API.Services
{
    public interface IMessageProducer
    {
        void Send<T>(T message);
    }
}