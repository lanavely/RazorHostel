using Microsoft.AspNetCore.Identity;

namespace Auto.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public int IdSchool { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        [PersonalData]
        public string? Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [PersonalData]
        public DateTime DOB { get; set; }

        public List<BookingEntity>? Bookings { get; set; }
    }
}
