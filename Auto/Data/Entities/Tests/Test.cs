namespace Auto.Data.Entities.Tests;

public class Test
{
    public int TestId { get; set; }
    
    public string? UserId { get; set; }

    public int TicketNumber { get; set; }
    
    public AppUser? User { get; set; }
    
    public List<TestQuestion>? Questions { get; set; }
}