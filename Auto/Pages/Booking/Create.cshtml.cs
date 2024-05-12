using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Auto.Data.Entities;

namespace Auto.Pages.Booking
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public CreateModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["IdSchool"] = new SelectList(_context.Schools, "IdSchool", "Name");
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "UserName");
            return Page();
        }

        [BindProperty]
        public Data.Entities.Booking Booking { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
