using System.ComponentModel;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Data.Entities.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        
        var teachers = (await _userManager.GetUsersInRoleAsync(Consts.Instructor))
            .Where(u => u.SchoolId == user.SchoolId)
            .Select(u => new SelectListItem(u.FullName, u.Id)).ToList();

        var schedule = await _context.Schedules.FirstAsync(s => s.SchoolId == user.SchoolId);
        
        ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName");
        ViewData["ScheduleItemId"] = new SelectList(schedule.ScheduleItems, "Id", "TimeString");
    }

    public async Task<IActionResult> OnPostSelectTimeAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var user = await _userManager.GetUserAsync(User);

        var schedule = await _context.Schedules.FirstAsync(s => s.SchoolId == user.SchoolId);
        var bookedSchedules = await _context.Bookings
            .Where(b => b.TeacherId == Model.TeacherId && b.Date == Model.Date)
            .Select(b => b.ScheduleItemId)
            .ToListAsync();
        Model.ScheduleItems = schedule.ScheduleItems.ExceptBy(bookedSchedules, c => c.ScheduleId).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? scheduleId)
    {
        if (scheduleId is null || !ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.GetUserAsync(User);

        var booking = new Booking()
        {
            TeacherId = Model.TeacherId,
            ClientId = user.Id,
            Date = Model.Date,
            SchoolId = user.SchoolId.Value,
            ScheduleItemId = scheduleId.Value
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    public class UserBookingCreateModel
    {
        [DisplayName("Дата")]
        public DateOnly Date { get; set; }
        
        [DisplayName("Инструктор")]
        public string TeacherId { get; set; }

        public List<ScheduleItem>? ScheduleItems { get; set; } = default!;
    }
}