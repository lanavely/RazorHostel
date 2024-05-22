using Auto.Enums;

namespace Auto.Data.Entities.Bookings;

public class Schedule
{
    public int ScheduleId { get; set; }
    
    public int SchoolId { get; set; }
    
    public DaysOfWeek Days { get; set; }

    public School? School { get; set; }
    
    public List<ScheduleItem> ScheduleItems { get; set; }
}