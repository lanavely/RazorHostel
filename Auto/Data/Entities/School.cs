using System.ComponentModel.DataAnnotations;

namespace Auto.Data.Entities
{
    public class School
    {
        public int SchoolId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<AppUser>? Users { get; set; }
        
        public List<Booking>? Bookings { get; set; }
    }
}
