using Microsoft.AspNetCore.Identity;

namespace Auto.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public int IdSchool { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public DateOnly Birthdate { get; set; }

        public string GroupNumber { get; set; }
        
        public List<Booking>? Bookings { get; set; }
    }
}
