using Auto.Data.Entities.Tests;
using Microsoft.AspNetCore.Identity;

namespace Auto.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public int? SchoolId { get; set; }

        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }

        public string? Patronymic { get; set; }

        public DateOnly? Birthdate { get; set; }

        public int? GroupId { get; set; }
        
        public Group? Group { get; set; }
        
        public School? School { get; set; }
        
        public List<Booking>? ClientBookings { get; set; }

        public List<Booking>? TeacherBookings { get; set; }
        
        public List<IdentityUserRole<string>> UserRoles { get; set; }
        
        public List<Test> Tests { get; set; }
    }
}
