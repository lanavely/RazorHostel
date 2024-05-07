using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Auto.Data;
using Auto.Data.Entities;

namespace Auto.Pages.School
{
    public class CreateModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public CreateModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SchoolEntity SchoolEntity { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Schools == null || SchoolEntity == null)
            {
                return Page();
            }

            _context.Schools.Add(SchoolEntity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
