using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hostel.DataAccess.Entities;
using RazorHostel.Data;

namespace RazorHostel.Pages.Room
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RoomEntity> RoomEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Rooms != null)
            {
                RoomEntity = await _context.Rooms
                .Include(r => r.Hostel).ToListAsync();
            }
        }
    }
}
