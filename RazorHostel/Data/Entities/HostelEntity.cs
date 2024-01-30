using System.ComponentModel.DataAnnotations;

namespace Hostel.DataAccess.Entities
{
    public class HostelEntity
    {
        public int IdHostel { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<RoomEntity>? Rooms { get; set; }
    }
}
