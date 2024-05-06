using Microsoft.AspNetCore.Identity;

namespace Auto.Data.Entities
{
    public class User : IdentityUser
    {
        public int IdSchool { get; set; }

        public List<BookingEntity>? Bookings { get; set; }
    }
}
