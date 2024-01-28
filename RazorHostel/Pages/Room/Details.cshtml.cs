using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hostel.DataAccess.Entities;
using RazorHostel.Data;

namespace RazorHostel.Pages.Room
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

      public RoomEntity RoomEntity { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var roomentity = await _context.Rooms.FirstOrDefaultAsync(m => m.IdRoom == id);
            if (roomentity == null)
            {
                return NotFound();
            }
            else 
            {
                RoomEntity = roomentity;
            }
            return Page();
        }
    }
}
