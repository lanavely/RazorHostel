using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data;
using Auto.Data.Entities;

namespace Auto.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public DeleteModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Users Users { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var Users = await _context.Users.FirstOrDefaultAsync(m => m.IdUser == id);

            if (Users == null)
            {
                return NotFound();
            }
            else 
            {
                Users = Users;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var Users = await _context.Users.FindAsync(id);

            if (Users != null)
            {
                Users = Users;
                _context.Users.Remove(Users);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
