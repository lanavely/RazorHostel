using System.ComponentModel;
using Auto.Data.Entities.Bookings;
using Auto.Data.Entities.Tests;
using Microsoft.AspNetCore.Identity;

namespace Auto.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public int? SchoolId { get; set; }

        [DisplayName("Фамилия")]
        public string? FirstName { get; set; }
        
        [DisplayName("Имя")]
        public string? LastName { get; set; }

        [DisplayName("Отчество")]
        public string? Patronymic { get; set; }

        [DisplayName("Дата рождения")]
        public DateOnly? Birthdate { get; set; }

        public int? GroupId { get; set; }
        
        [DisplayName("Группа")]
        public Group? Group { get; set; }
        
        [DisplayName("Автошкола")]
        public School? School { get; set; }
        
        public List<Booking>? ClientBookings { get; set; }

        public List<Booking>? TeacherBookings { get; set; }
        
        public List<IdentityUserRole<string>> UserRoles { get; set; }
        
        public List<Test> Tests { get; set; }

        [DisplayName("ФИО")]
        public string FullName => $"{LastName} {FirstName} {Patronymic}";
    }
}
