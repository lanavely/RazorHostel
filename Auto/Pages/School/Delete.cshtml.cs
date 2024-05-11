using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data.Entities;

namespace Auto.Pages.School
{
    public class DeleteModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public DeleteModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public SchoolEntity SchoolEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Schools == null)
            {
                return NotFound();
            }

            var schoolentity = await _context.Schools.FirstOrDefaultAsync(m => m.IdSchool == id);

            if (schoolentity == null)
            {
                return NotFound();
            }
            else 
            {
                SchoolEntity = schoolentity;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Schools == null)
            {
                return NotFound();
            }
            var schoolentity = await _context.Schools.FindAsync(id);

            if (schoolentity != null)
            {
                SchoolEntity = schoolentity;
                _context.Schools.Remove(SchoolEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
