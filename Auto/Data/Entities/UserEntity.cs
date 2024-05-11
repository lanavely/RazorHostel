using Microsoft.AspNetCore.Identity;

namespace Auto.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public int IdSchool { get; set; }

        public List<BookingEntity>? Bookings { get; set; }
    }
}
