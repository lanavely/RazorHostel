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
    public class DetailsModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public DetailsModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
