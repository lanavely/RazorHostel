using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Data.Entities.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Auto.Pages.Bookings
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Booking> Bookings { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (_context.Bookings == null)
            {
                return;
            }

            var bookings = _context.Bookings
                .Include(b => b.Client)
                .Include(b => b.School)
                .Include(b => b.Teacher)
                .Include(b => b.ScheduleItem);
 
            if (await _userManager.IsInRoleAsync(user, Consts.Instructor))
            {
                bookings.Where(b => b.TeacherId == user.Id);
            }

            if (await _userManager.IsInRoleAsync(user, Consts.Student))
            {
                bookings.Where(b => b.ClientId == user.Id);
            }

            if (await _userManager.IsInRoleAsync(user, Consts.Admin))
            {
                bookings.Where(b => b.SchoolId == user.SchoolId);
            }

            Bookings = await bookings.ToListAsync();
        }
    }
}
