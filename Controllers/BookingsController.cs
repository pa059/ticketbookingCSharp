using ticketbooking.Data;
using ticketbooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace CruiseBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Book([FromBody] Booking booking)
        {
            var cruise = _context.Cruises.Find(booking.CruiseId);
            if (cruise == null || cruise.AvailableSeats < booking.NumberOfSeats)
                return BadRequest("Not enough seats available.");

            booking.TotalPrice = cruise.Price * booking.NumberOfSeats;
            booking.BookingDate = DateTime.Now;
            booking.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            cruise.AvailableSeats -= booking.NumberOfSeats;
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return Ok(booking);
        }

        [HttpGet]
        public IActionResult GetMyBookings()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var bookings = _context.Bookings.Where(b => b.UserId == userId).ToList();
            return Ok(bookings);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllBookings()
        {
            return Ok(_context.Bookings.ToList());
        }
    }
}
