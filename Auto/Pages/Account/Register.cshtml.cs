using Auto.Data.Entities;
using Auto.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auto.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public RegisterModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    [BindProperty]
    public UserRegisterModel Input { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public async Task OnGetAsync()
    {
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        var currentUser = await _userManager.GetUserAsync(User);
        
        if (!ModelState.IsValid)
            return Page();

        var user = _mapper.Map<AppUser>(Input);

        var result = await _userManager.CreateAsync(user, Input.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Consts.Student);
            if (currentUser is not null)
            {
                return RedirectToPage("/Users/Index");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToPage("/Index");
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}
