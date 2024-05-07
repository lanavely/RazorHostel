using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Auto.Data;
using Auto.Data.Entities;

namespace Auto.Pages.Booking
{
    public class CreateModel : PageModel
    {
        private readonly Auto.Data.ApplicationDbContext _context;

        public CreateModel(Auto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdSchool"] = new SelectList(_context.Schools, "IdSchool", "IdSchool");
        ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id");
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
