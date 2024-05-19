using System.Reflection.Metadata.Ecma335;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Data.Entities.Tests;
using Auto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auto.Pages.Tests
{
    [Authorize]
    public class ExecutingModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly TestService _service;
        
        public ExecutingModel(
            UserManager<AppUser> userManager,
            ApplicationDbContext dbContext,
            TestService service)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _service = service;
        }

        [BindProperty] public TestViewModel TestModel { get; set; } = new TestViewModel();
        
        public async Task OnGetAsync(int? ticket, int? testId, int? questionNumber)
        {
            var user = await _userManager.GetUserAsync(User);

            if (testId == null)
            {
                TestModel.Test = await _service.CreateTestForTicket(ticket ?? 1, user);
            }
            else
            {
                TestModel.Test = await _service.GetTestByIdAsync(testId.Value);
            }
            
            if (questionNumber == null || questionNumber < 0 || questionNumber >= TestModel.Test.Questions.Count)
            {
                questionNumber = 0;
            }

            if (questionNumber != TestModel.Test.CurrentQuestionNumber)
            {
                TestModel.Test.CurrentQuestionNumber = questionNumber.Value;
                _dbContext.Tests.Update(TestModel.Test);
                await _dbContext.SaveChangesAsync();    
            }
        }

        public async Task<IActionResult> OnPostSelectAsync(int? answerId)
        {
            TestModel.Test = await _service.GetTestByIdAsync(TestModel.TestId);

            if (answerId is null || TestModel.CurrentQuestion.AnswerId is not null)
            {
                return Page();
            }

            TestModel.CurrentQuestion.AnswerId = answerId;
            TestModel.Test.CurrentQuestionNumber = (TestModel.Test.CurrentQuestionNumber + 1) % TestModel.Test.Questions.Count;
            _dbContext.Tests.Update(TestModel.Test);
            await _dbContext.SaveChangesAsync();
            
            return Page();
        }

        public async Task<IActionResult> OnPostRestartAsync(int testId)
        {
            TestModel.Test = await _service.GetTestByIdAsync(testId);
            return Page();
        }

        public class TestViewModel
        {
            private Test? _test;

            public bool? ShowCloseTest => _test?.Questions?.All(q => q.AnswerId is not null);

            public int TestId { get; set; }

            public Test? Test
            {
                get => _test;
                set
                {
                    _test = value;
                    TestId = value.TestId;
                }
            }

            public TestQuestion CurrentQuestion => Test?.Questions[Test.CurrentQuestionNumber];
        }
    }
}
