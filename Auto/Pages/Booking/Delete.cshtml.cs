using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data.Entities;

namespace Auto.Pages.Booking
{
    public class DeleteModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public DeleteModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookingEntity BookingEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingEntity = await _context.Bookings.FirstOrDefaultAsync(m => m.IdBooking == id);

            if (bookingEntity == null)
            {
                return NotFound();
            }
            else 
            {
                BookingEntity = bookingEntity;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookingEntity = await _context.Bookings.FindAsync(id);

            if (bookingEntity != null)
            {
                BookingEntity = bookingEntity;
                _context.Bookings.Remove(BookingEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
