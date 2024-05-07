using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data;
using Auto.Data.Entities;

namespace Auto.Pages.Booking
{
    public class IndexModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public IndexModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BookingEntity> BookingEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Bookings != null)
            {
                BookingEntity = await _context.Bookings
                .Include(b => b.School)
                .Include(b => b.User).ToListAsync();
            }
        }
    }
}
