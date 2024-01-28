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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<HostelEntity> HostelEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Hostels != null)
            {
                HostelEntity = await _context.Hostels.ToListAsync();
            }
        }
    }
}
