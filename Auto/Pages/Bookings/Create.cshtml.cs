using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Auto.Data;
using Auto.Data.Entities;

namespace Auto.Pages.Bookings
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
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName");

            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Bookings == null || Booking == null)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
