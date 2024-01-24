namespace Hostel.DataAccess.Entities
{
    public class Booking
    {
        public int IdBooking { get; set; }

        public string Comment { get; set; }

        public int IdUser { get; set; }

        public Client User { get; set; }

        public int IdRoom { get; set; }

        public Room Room { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
