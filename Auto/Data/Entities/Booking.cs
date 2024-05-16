namespace Auto.Data.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string IdUser { get; set; }
        
        public int SchoolId { get; set; }
        
        public string? Comment { get; set; }

        public AppUser? User { get; set; }
        
        public School? School { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
