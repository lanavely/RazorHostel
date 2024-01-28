using System.ComponentModel.DataAnnotations;

namespace Hostel.DataAccess.Entities
{
    public class ClientEntity
    {
        public int IdClient { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Patronymic { get; set; }

        public string FullName => $"{LastName} {FirstName} {Patronymic}";

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public List<BookingEntity>? Bookings { get; set; }
    }
}
