using System.ComponentModel;

namespace Hostel.DataAccess.Entities
{
    public class BookingEntity
    {
        public int IdBooking { get; set; }

        public string? Comment { get; set; }

        [DisplayName("Client")]
        public int IdClient { get; set; }

        public ClientEntity? Client { get; set; }

        [DisplayName("Room")]
        public int IdRoom { get; set; }

        public RoomEntity? Room { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
