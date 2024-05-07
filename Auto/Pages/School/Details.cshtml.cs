using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data;
using Auto.Data.Entities;

namespace Auto.Pages.School
{
    public class DetailsModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public DetailsModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
