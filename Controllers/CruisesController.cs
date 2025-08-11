using ticketbooking.Data;
using ticketbooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ticketbooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CruisesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CruisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCruises() => Ok(_context.Cruises.ToList());

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetCruise(int id)
        {
            var cruise = _context.Cruises.Find(id);
            if (cruise == null) return NotFound();
            return Ok(cruise);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCruise([FromBody] Cruise cruise)
        {
            _context.Cruises.Add(cruise);
            _context.SaveChanges();
            return Ok(cruise);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCruise(int id, [FromBody] Cruise cruise)
        {
            var existing = _context.Cruises.Find(id);
            if (existing == null) return NotFound();

            existing.CruiseName = cruise.CruiseName;
            existing.Price = cruise.Price;
            existing.AvailableSeats = cruise.AvailableSeats;
            existing.DepartureDate = cruise.DepartureDate;
            existing.Destination = cruise.Destination;
            existing.DeparturePort = cruise.DeparturePort;

            _context.SaveChanges();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCruise(int id)
        {
            var cruise = _context.Cruises.Find(id);
            if (cruise == null) return NotFound();

            _context.Cruises.Remove(cruise);
            _context.SaveChanges();
            return Ok();
        }
    }
}
