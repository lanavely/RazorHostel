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

namespace RazorHostel.Pages.Room
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RoomEntity RoomEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var roomentity =  await _context.Rooms.FirstOrDefaultAsync(m => m.IdRoom == id);
            if (roomentity == null)
            {
                return NotFound();
            }
            RoomEntity = roomentity;
            ViewData["IdHostel"] = new SelectList(_context.Hostels, "IdHostel", "Name");
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

            _context.Attach(RoomEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomEntityExists(RoomEntity.IdRoom))
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

        private bool RoomEntityExists(int id)
        {
          return (_context.Rooms?.Any(e => e.IdRoom == id)).GetValueOrDefault();
        }
    }
}
