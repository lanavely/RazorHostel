﻿using System;
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
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookingentity = await _context.Bookings.FirstOrDefaultAsync(m => m.IdBooking == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }
            var bookingentity = await _context.Bookings.FindAsync(id);

            if (bookingentity != null)
            {
                BookingEntity = bookingentity;
                _context.Bookings.Remove(BookingEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
