using System.ComponentModel;

namespace Auto.Models;

public class AdminUserEditModel: UserEditModel
{
    public string Id { get; set; }

    [DisplayName("Автошкола")]
    public int? SchoolId { get; set; }
    
    public string? RoleName { get; set; }
}