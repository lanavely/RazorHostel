using System.ComponentModel;

namespace Auto.Data.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string ClientId { get; set; }

        public string TeacherId { get; set; }
        
        public int SchoolId { get; set; }
        
        [DisplayName("Закрыто")]
        public bool isClosed { get; set; }
        
        [DisplayName("Комментарий")]
        public string? Comment { get; set; }

        [DisplayName("Время начала")]
        public DateTime StartDate { get; set; }

        [DisplayName("Время окончания")]
        public DateTime EndDate { get; set; }

        [DisplayName("Студент")]
        public AppUser? Client { get; set; }
        
        [DisplayName("Инструктор")]
        public AppUser? Teacher { get; set; }
        
        public School? School { get; set; }
    }
}
