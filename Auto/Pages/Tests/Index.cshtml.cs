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
        
        public int QuestionIndex { get; set; }

        public int TicketNumber { get; set; }

        public TestQuestion CurrentQuestion => TestModel.Questions[QuestionIndex];
        
        public async Task OnGetAsync(int ticket)
        {
            TicketNumber = ticket;
            AppUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.GetUserAsync(User);
            }
            TestModel = await _service.GetTestAsync(TicketNumber, user);
        }

        public async Task OnPostSelectAsync(int number, int ticket)
        {
            QuestionIndex = number;

            OnGetAsync(TicketNumber);
        }
    }
}
