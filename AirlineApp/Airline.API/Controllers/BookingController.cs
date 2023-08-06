using Airline.API.Models;
using Airline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IMessageProducer _messageProducer;

        // In-memory Database
        public static readonly List<Booking> _bookings = new();

        public BookingController(ILogger<BookingController> logger, IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost(Name = "send")]
        public IActionResult Send(Booking booking)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _bookings.Add(booking);

            _messageProducer.Send(booking);

            return Ok();
        }
    }
}