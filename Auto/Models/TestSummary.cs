using Auto.Enums;
using Auto.Helpers;

namespace Auto.Models;

public class TestSummary
{
    public int TicketNumber { get; set; }
    
    public Status Status { get; set; }

    public string StatusColor => BackgroundHelper.GetColor(Status);
}