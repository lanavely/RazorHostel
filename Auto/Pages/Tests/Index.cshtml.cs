using Auto.Data;
using Auto.Data.Entities;
using Auto.Data.Entities.Tests;
using Auto.Enums;
using Auto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auto.Pages.Tests
{
    [Authorize]
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
        
        public List<Ticket> Tickets { get; set; }
        
        public async Task OnGetAsync()
        {
            AppUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.GetUserAsync(User);
            }

            var tests = await _service.GetTestForTicketsAsync(user);
            var tickets = Enumerable.Range(1, 40).Select(i => new Ticket()
            {
                TicketNumber = i,
                Test = tests.FirstOrDefault(t => t.TicketNumber == i)
            }).ToList();

            Tickets = tickets;
        }
        
        public class Ticket
        {
            public int TicketNumber { get; set; }
            public Test? Test { get; set; }
            public Status Status
            {
                get
                {
                    if (Test == null)
                        return Status.Unknown;
                    if (Test.Questions.Any(q => q.Answer is null))
                        return Status.InProcess;
                    if (Test.Questions.All(q => q.Answer?.IsCorrect == true))
                    {
                        return Status.Success;
                    }

                    return Status.Fail;
                }
            }
        }
    }
}
