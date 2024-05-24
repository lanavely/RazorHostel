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
    
    public async Task OnGetAsync(string studentId, string teacherId, DateOnly date)
    {
        var user = await _userManager.GetUserAsync(User);
        Model.TeacherId = teacherId;
        Model.StudentId = studentId;
        Model.Student = await _userManager.FindByIdAsync(studentId);
        Model.Teacher = await _userManager.FindByIdAsync(teacherId);
        Model.Date = date;

        var schedule = await _context.Schedules
            .Include(s => s.ScheduleItems)
            .FirstAsync(s => s.SchoolId == user.SchoolId);
        var bookedSchedules = await _context.Bookings
            .Where(b => b.TeacherId == teacherId && b.Date == date)
            .Select(b => b.ScheduleItemId)
            .ToListAsync();
        ViewData["ScheduleItems"] = schedule.ScheduleItems.ExceptBy(bookedSchedules, c => c.Id).OrderBy(s => s.StartTime).ToList();
    }

    public async Task<IActionResult> OnPostAsync(int? scheduleId)
    {
        if (scheduleId is null || !ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Невалидная модель");
            return Page();
        }

        var user = await _userManager.FindByIdAsync(Model.StudentId);

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
            ModelState.AddModelError(string.Empty, "Время бронирования уже занято");
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
        
        public AppUser? Teacher { get; set; }
        
        public AppUser? Student { get; set; }
    }
}