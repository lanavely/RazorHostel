using Auto.Data;
using Auto.Data.Entities.Tests;
using Auto.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auto.Pages.Tests
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TestService _service;

        public IndexModel(
            ApplicationDbContext dbContext,
            TestService service)
        {
            _dbContext = dbContext;
            _service = service;
        }
        
        public Test TestModel { get; set; }
        
        public async Task OnGetAsync()
        {
            var random = Convert.ToInt32(new Random().NextInt64(25));
            TestModel = await _service.GetTestAsync(random);
        }
    }
}
