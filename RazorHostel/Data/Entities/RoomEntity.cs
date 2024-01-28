using System.ComponentModel;

namespace Hostel.DataAccess.Entities
{
    public class RoomEntity
    {
        public int IdRoom { get; set; }

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public string Name { get; set; }

        public string Capacity { get; set; }

        public decimal Square { get; set; }

        [DisplayName("Hostel")]
        public int IdHostel { get; set; }

        public decimal Price { get; set; }

        public HostelEntity? Hostel { get; set; }

        public List<BookingEntity>? Bookings { get; set; }
    }
}
