using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data.Entities;

namespace Auto.Pages.Booking
{
    public class DetailsModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public DetailsModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public BookingEntity BookingEntity { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookingentity = await _context.Bookings.Include(b => b.User)
                .Include(b => b.School)
                .FirstOrDefaultAsync(m => m.IdBooking == id);

            if (bookingentity == null)
            {
                return NotFound();
            }
            else 
            {
                BookingEntity = bookingentity;
            }
            return Page();
        }
    }
}
