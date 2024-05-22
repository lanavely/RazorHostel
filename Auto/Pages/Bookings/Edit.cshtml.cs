using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Data.Entities.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Auto.Pages.Bookings
{
    [Authorize(Roles = $"{Consts.Admin},{Consts.Instructor}")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EditModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking =  await _context.Bookings.FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            
            var user = await _userManager.GetUserAsync(User);
            var schedule = await _context.Schedules
                .Include(s => s.ScheduleItems)
                .FirstAsync(s => s.SchoolId == user.SchoolId);

            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["SchoolId"] = new SelectList(_context.Schools.Where(s => s.SchoolId == user.SchoolId), "SchoolId", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["ScheduleItemId"] = new SelectList(schedule.ScheduleItems, "Id", "TimeString");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(Booking.BookingId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookingExists(int id)
        {
            return (_context.Bookings?.Any(e => e.BookingId == id)).GetValueOrDefault();
        }
    }
}
