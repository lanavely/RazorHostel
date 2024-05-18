using Auto.Data;
using Auto.Data.Entities;
using Auto.Data.Entities.Tests;
using Auto.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auto.Pages.Tests
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly TestService _service;
        private int questionIndex { get; set; }
        private int ticketNumber { get; set; }
        
        public IndexModel(
            UserManager<AppUser> userManager,
            ApplicationDbContext dbContext,
            TestService service)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _service = service;
        }
        
        [BindProperty]
        public Test TestModel { get; set; }

        public TestQuestion CurrentQuestion => TestModel.Questions[questionIndex];
        
        public async Task OnGetAsync(int ticket)
        {
            AppUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.GetUserAsync(User);
            }
            TestModel = await _service.GetTestAsync(ticket, user);
        }

        public async Task OnPostSelectAsync(int number)
        {
            questionIndex = number;

            OnGetAsync(ticket);
        }
    }
}
