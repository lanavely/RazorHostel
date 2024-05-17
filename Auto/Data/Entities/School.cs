using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Auto.Data.Entities
{
    public class School
    {
        public int SchoolId { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        public string Address { get; set; }

        [Phone]
        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }

        public List<AppUser>? Users { get; set; }
        
        public List<Booking>? Bookings { get; set; }
    }
}
