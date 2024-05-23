using Auto.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auto.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DeleteModel(
            Data.ApplicationDbContext context,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

      [BindProperty]
      public AppUser AppUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            AppUser = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            var result = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));

            return RedirectToPage("./Index");
        }
    }
}
