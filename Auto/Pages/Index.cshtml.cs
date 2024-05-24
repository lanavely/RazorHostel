using Auto.Data;
using Auto.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Auto.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            UserManager<AppUser> userManager,
            ApplicationDbContext applicationDbContext,
            ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }
        
        public School? School { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is { SchoolId: not null })
            {
                School = await _applicationDbContext.Schools.FirstAsync(s => s.SchoolId == user.SchoolId);
            }
        }
    }
}
