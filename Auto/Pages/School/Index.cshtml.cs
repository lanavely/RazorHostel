using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data.Entities;

namespace Auto.Pages.School
{
    public class IndexModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public IndexModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SchoolEntity> SchoolEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Schools != null)
            {
                SchoolEntity = await _context.Schools.ToListAsync();
            }
        }
    }
}
