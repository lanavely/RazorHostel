using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hostel.DataAccess.Entities;
using RazorHostel.Data;

namespace RazorHostel.Pages.Booking
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookingEntity BookingEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookingentity =  await _context.Bookings.FirstOrDefaultAsync(m => m.IdBooking == id);
            if (bookingentity == null)
            {
                return NotFound();
            }
            BookingEntity = bookingentity;
           ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "Name");
           ViewData["IdClient"] = new SelectList(_context.Users, "IdClient", "FullName");
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
