namespace ticketbooking.Models
{
    public class Cruise
    {
        public int CruiseId { get; set; }
        public string CruiseName { get; set; }
        public string DeparturePort { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
