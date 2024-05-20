using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data.Entities;

namespace Auto.Pages.Schools
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<School> School { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Schools != null)
            {
                School = await _context.Schools.ToListAsync();
            }
        }
    }
}
