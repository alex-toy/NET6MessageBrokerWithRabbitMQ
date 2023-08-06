using Airline.API.Models;
using MessageQueueHandler;
using Microsoft.AspNetCore.Mvc;

namespace Airline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageQueueSender _messageProducer;

        public MessageController(IMessageQueueSender messageProducer)
        {
            _messageProducer = new MessageQueueSender("demoExchange", "demo-routing-key", "demoQueue");
        }

        [HttpPost(Name = "send")]
        public IActionResult Send(Booking booking)
        {
            _messageProducer.Send<Booking>(booking);

            return Ok();
        }
    }
}