using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hostel.DataAccess.Entities;

namespace RazorHostel.Pages.Room
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
        ViewData["IdHostel"] = new SelectList(_context.Hostels, "IdHostel", "Name");
            return Page();
        }

        [BindProperty]
        public RoomEntity RoomEntity { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Rooms == null || RoomEntity == null)
            {
                return Page();
            }

            _context.Rooms.Add(RoomEntity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
