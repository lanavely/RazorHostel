using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Auto.Data.Entities;

namespace Auto.Pages.Schools
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public CreateModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public School School { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Schools == null || School == null)
            {
                return Page();
            }

            _context.Schools.Add(School);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
