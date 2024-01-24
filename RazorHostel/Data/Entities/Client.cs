namespace Hostel.DataAccess.Entities
{
    public class Client
    {
        public int IdUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
