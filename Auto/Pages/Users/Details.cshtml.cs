using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data.Entities;
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
        private readonly IMapper _mapper;

        public DetailsModel(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

      public DetailsUserModel UserModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users
                .Include(u => u.School)
                .Include(b => b.UserRoles)
                .Include(b => b.Tests)
                .Include(u => u.ClientBookings)
                .ThenInclude(b => b.Teacher)
                .Include(u => u.ClientBookings)
                .ThenInclude(b => b.ScheduleItem)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            UserModel = _mapper.Map<DetailsUserModel>(user);
            UserModel.RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if (user.Tests.Any())
            {
                UserModel.Tests = user.Tests.OrderBy(t => t.TicketNumber).Select(t =>
                    new TestSummary()
                    {
                        TicketNumber = t.TicketNumber,
                        Status = t.PassedStatus
                    }).ToList();
            }

            return Page();
        }
    }
}
