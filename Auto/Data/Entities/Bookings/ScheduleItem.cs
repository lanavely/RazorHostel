using System.ComponentModel;

namespace Auto.Data.Entities.Bookings;

public class ScheduleItem
{
    public int Id { get; set; }

    public int ScheduleId { get; set; }
    
    [DisplayName("Время начала")]
    public TimeOnly StartTime { get; set; }

    [DisplayName("Время окончания")]
    public DateTime EndTime { get; set; }

    [DisplayName("Расписание")]
    public string TimeString => $"{StartTime} - {EndTime}";
    
    public Schedule? Schedule { get; set; }
}