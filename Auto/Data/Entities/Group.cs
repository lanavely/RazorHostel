using System.ComponentModel;

namespace Auto.Data.Entities;

public class Group
{
    public int GroupId { get; set; }
    
    [DisplayName("Имя группы")]
    public string Name { get; set; }
    
    public List<AppUser>? Users { get; set; }
}