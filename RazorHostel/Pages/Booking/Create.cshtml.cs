using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hostel.DataAccess.Entities;
using RazorHostel.Data;

namespace RazorHostel.Pages.Booking
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "Name");
        ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "FullName");
            return Page();
        }

        [BindProperty]
        public BookingEntity BookingEntity { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Bookings == null || BookingEntity == null)
            {
                return Page();
            }

            _context.Bookings.Add(BookingEntity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
