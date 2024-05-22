using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Data.Entities.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auto.Pages.Bookings
{
    [Authorize(Roles = $"{Consts.Admin},{Consts.Instructor}")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var schedule = await _context.Schedules
                .Include(s => s.ScheduleItems)
                .FirstAsync(s => s.SchoolId == user.SchoolId);

            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["SchoolId"] = new SelectList(_context.Schools.Where(s => user.SchoolId == null || s.SchoolId == user.SchoolId), "SchoolId", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["ScheduleItemId"] = new SelectList(schedule.ScheduleItems, "Id", "TimeString");

            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Bookings == null || Booking == null)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
