using ticketbooking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ticketbooking.Models;
namespace ticketbooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cruise> Cruises { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
