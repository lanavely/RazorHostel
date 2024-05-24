using System.ComponentModel;

namespace Auto.Data.Entities.Bookings
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string ClientId { get; set; }

        public string TeacherId { get; set; }
        
        public int ScheduleItemId { get; set; }
        
        public int SchoolId { get; set; }
        
        [DisplayName("Комментарий")]
        public string? Comment { get; set; }

        [DisplayName("Дата")]
        public DateOnly Date { get; set; }
        
        [DisplayName("Время")]
        public ScheduleItem? ScheduleItem { get; set; }

        [DisplayName("Ученик")]
        public AppUser? Client { get; set; }
        
        [DisplayName("Инструктор")]
        public AppUser? Teacher { get; set; }
        
        [DisplayName("Автошкола")]
        public School? School { get; set; }
    }
}
