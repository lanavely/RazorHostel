namespace Hostel.DataAccess.Entities
{
    public class Hostel
    {
        public int IdHostel { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
