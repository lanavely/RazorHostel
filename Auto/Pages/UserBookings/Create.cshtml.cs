using Auto.Data;
using Auto.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Auto.Pages.UserBookings;

[Authorize]
public class Create : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public Create(
        ApplicationDbContext context,
        UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty] public UserBookingCreateModel Model { get; set; } = new();
    
    public async void OnGetAsync()
    {
        Model.Teachers = (await _userManager.GetUsersInRoleAsync(Consts.Instructor)).
            Select(u => new SelectListItem(u.FullName, u.Id)).ToList();
    }

    public class UserBookingCreateModel
    {
        public DateOnly Date { get; set; }
        
        public TimeOnly Time { get; set; }
        
        public int TeacherId { get; set; }
        
        public List<SelectListItem> Teachers { get; set; }
    }
}