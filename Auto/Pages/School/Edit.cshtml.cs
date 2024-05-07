using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auto.Data;
using Auto.Data.Entities;

namespace Auto.Pages.School
{
    public class EditModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public EditModel(Auto.Data.ApplicationDbContext context)
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

            var schoolentity =  await _context.Schools.FirstOrDefaultAsync(m => m.IdSchool == id);
            if (schoolentity == null)
            {
                return NotFound();
            }
            SchoolEntity = schoolentity;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SchoolEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolEntityExists(SchoolEntity.IdSchool))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SchoolEntityExists(int id)
        {
          return (_context.Schools?.Any(e => e.IdSchool == id)).GetValueOrDefault();
        }
    }
}
