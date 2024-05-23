using System.ComponentModel;

namespace Auto.Models;

public class AdminUserEditModel: UserEditModel
{
    [DisplayName("Автошкола")]
    public int? SchoolId { get; set; }
    
    public string? Role { get; set; }
}