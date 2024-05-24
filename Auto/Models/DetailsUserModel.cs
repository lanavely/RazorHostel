using System.ComponentModel;
using Auto.Data.Entities.Bookings;
using Auto.Helpers;

namespace Auto.Models;

public class DetailsUserModel: UserModel
{
    public string Id { get; set; }
    
    [DisplayName("Роль")]
    public string? RoleName { get; set; }
    
    [DisplayName("Автошкола")]
    public string SchoolName { get; set; }
    
    public List<TestSummary>? TestSummaries { get; set; }
    
    public List<Booking>? Bookings { get; set; }

    [DisplayName("Роль")]
    public string RoleString => RoleNameHelper.RoleToString(RoleName);
}