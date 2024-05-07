using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Auto.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auto.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;

        public CreateModel(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserEntity Users { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Users == null || Users == null)
            {
                return Page();
            }

            _userManager.Add(Users);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
