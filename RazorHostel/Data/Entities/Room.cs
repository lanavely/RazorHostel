namespace Hostel.DataAccess.Entities
{
    public class Room
    {
        public int IdRoom { get; set; }

        public string Comment { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string Capacity { get; set; }

        public decimal Square { get; set; }

        public int IdHostel { get; set; }

        public Hostel Hostel { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
