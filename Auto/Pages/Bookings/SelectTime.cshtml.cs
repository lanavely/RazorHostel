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
public class SelectTime : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public SelectTime(
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
            students.AddRange((await _userManager.GetUsersInRoleAsync(Consts.Student))
                .Where(u => u.SchoolId == user.SchoolId));
        }
        
        var teachers = (await _userManager.GetUsersInRoleAsync(Consts.Instructor))
            .Where(u => u.SchoolId == user.SchoolId).ToList();

        ViewData["TeacherId"] = new SelectList(teachers, "Id", "FullName");
        ViewData["StudentId"] = new SelectList(students, "Id", "FullName");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return RedirectToPage("./Create",
            new { teacherId = Model.TeacherId, studentId = Model.StudentId, date = Model.Date });
    }

    public class UserBookingCreateModel
    {
        [DisplayName("Дата")] 
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        
        [DisplayName("Инструктор")]
        public string TeacherId { get; set; }
        
        [DisplayName("Студент")]
        public string StudentId { get; set; }
    }
}