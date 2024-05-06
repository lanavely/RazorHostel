using System.ComponentModel.DataAnnotations;

namespace Auto.Data.Entities
{
    public class SchoolEntity
    {
        public int IdSchool { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<User>? Users { get; set; }
        
        public List<BookingEntity>? Bookings { get; set; }
    }
}
