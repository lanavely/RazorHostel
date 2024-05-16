namespace Auto.Data.Entities;

public class Group
{
    public int GroupId { get; set; }
    
    public string Name { get; set; }
    
    public List<AppUser>? Users { get; set; }
}