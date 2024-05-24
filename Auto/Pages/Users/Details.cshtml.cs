using Auto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data.Entities;
using Auto.Helpers;
using Auto.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Auto.Pages.Users
{
    [Authorize(Roles = Consts.AdminInstructor)]
    public class DetailsModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DetailsModel(UserManager<AppUser> userManager, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

      public DetailsUserModel UserModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.Where(b => b.ClientId == id || b.TeacherId == id)
                .Include(b => b.Client)
                .Include(b => b.Teacher)
                .Include(b => b.ScheduleItem)
                .ToListAsync();

            var user = await _userManager.Users
                .Include(u => u.School)
                .Include(b => b.UserRoles)
                .Include(b => b.Tests)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            UserModel = _mapper.Map<DetailsUserModel>(user);
            UserModel.RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            UserModel.Bookings = bookings;

            if (user.Tests.Any())
            {
                UserModel.TestSummaries = user.Tests.OrderBy(t => t.TicketNumber).Select(t =>
                    new TestSummary()
                    {
                        TicketNumber = t.TicketNumber,
                        Status = t.PassedStatus
                    })
                    .OrderBy(t => t.TicketNumber)
                    .ToList();
            }

            return Page();
        }
    }
}
