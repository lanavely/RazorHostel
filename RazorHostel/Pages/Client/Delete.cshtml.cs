using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hostel.DataAccess.Entities;
using RazorHostel.Data;

namespace RazorHostel.Pages.Client
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ClientEntity ClientEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var cliententity = await _context.Clients.FirstOrDefaultAsync(m => m.IdClient == id);

            if (cliententity == null)
            {
                return NotFound();
            }
            else 
            {
                ClientEntity = cliententity;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }
            var cliententity = await _context.Clients.FindAsync(id);

            if (cliententity != null)
            {
                ClientEntity = cliententity;
                _context.Clients.Remove(ClientEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
