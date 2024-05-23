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

        var students = new List<AppUser>();

        if (await _userManager.IsInRoleAsync(user, Consts.Student))
        {
           students.Add(user); 
        }
        else
        {
            students.AddRange((await _userManager.GetUsersInRoleAsync(Consts.Instructor))
                .Where(u => u.SchoolId == user.SchoolId));
        }
        
        var teachers = (await _userManager.GetUsersInRoleAsync(Consts.Instructor))
            .Where(u => u.SchoolId == user.SchoolId).ToList();

        ViewData["TeacherId"] = new SelectList(teachers, "Id", "FullName");
        ViewData["StudentId"] = new SelectList(students, "Id", "FullName");
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
        ViewData["ScheduleItems"] = schedule.ScheduleItems.ExceptBy(bookedSchedules, c => c.ScheduleId).ToList();

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

        var isExist = await _context.Bookings.AnyAsync(b =>
            b.TeacherId == b.TeacherId && 
            b.ClientId == b.ClientId && 
            b.Date == booking.Date && 
            b.ScheduleItemId == booking.ScheduleItemId
        );

        if (isExist)
        {
            return Page();
        }

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Bookings/Index");
    }

    public class UserBookingCreateModel
    {
        [DisplayName("Дата")]
        public DateOnly Date { get; set; }
        
        [DisplayName("Инструктор")]
        public string TeacherId { get; set; }
        
        [DisplayName("Студент")]
        public string StudentId { get; set; }
    }
}