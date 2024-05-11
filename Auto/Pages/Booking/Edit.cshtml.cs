using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auto.Data.Entities;

namespace Auto.Pages.Booking
{
    public class EditModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public EditModel(Auto.Data.ApplicationDbContext context)
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

            var bookingEntity =  await _context.Bookings.FirstOrDefaultAsync(m => m.IdBooking == id);
            if (bookingEntity == null)
            {
                return NotFound();
            }
            BookingEntity = bookingEntity;
            ViewData["IdSchool"] = new SelectList(_context.Schools, "IdSchool", "Name");
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "UserName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BookingEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingEntityExists(BookingEntity.IdBooking))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookingEntityExists(int id)
        {
          return (_context.Bookings?.Any(e => e.IdBooking == id)).GetValueOrDefault();
        }
    }
}
