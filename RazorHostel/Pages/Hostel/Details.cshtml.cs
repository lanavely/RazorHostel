using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hostel.DataAccess.Entities;
using RazorHostel.Data;

namespace RazorHostel.Pages.Hostel
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

      public HostelEntity HostelEntity { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Hostels == null)
            {
                return NotFound();
            }

            var hostelentity = await _context.Hostels.FirstOrDefaultAsync(m => m.IdHostel == id);
            if (hostelentity == null)
            {
                return NotFound();
            }
            else 
            {
                HostelEntity = hostelentity;
            }
            return Page();
        }
    }
}
