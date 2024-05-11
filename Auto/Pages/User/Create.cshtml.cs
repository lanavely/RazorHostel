using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Auto.Data;
using Auto.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auto.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public CreateModel(
            ApplicationDbContext context,
            UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["IdSchool"] = new SelectList(_context.Schools, "IdSchool", "Name");
            ViewData["IdBooking"] = new SelectList(_context.Bookings, "IdBooking", "StartDate");
                return Page();
        }

        [BindProperty]
        public InputModel UserModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new UserEntity()
            {
                UserName = UserModel.Username,
                Email = UserModel.Email,
                PhoneNumber = UserModel.PhoneNumber
            };

            await _userManager.CreateAsync(user, UserModel.Password);

            return RedirectToPage("./Index");
        }
        
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full name")]
            public string Username { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone")]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
