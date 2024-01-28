using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hostel.DataAccess.Entities;
using RazorHostel.Data;

namespace RazorHostel.Pages.Booking
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BookingEntity> BookingEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Bookings != null)
            {
                BookingEntity = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Client).ToListAsync();
            }
        }
    }
}
