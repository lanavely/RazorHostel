using Auto.Data.Entities;
using Auto.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auto.Pages.Account;

public class ManageModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;

    public ManageModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public UserEditModel Input { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        Input = _mapper.Map<UserEditModel>(user);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Не найден пользователь с ID '{_userManager.GetUserId(User)}'.");
        }

        if (Input.Password is not null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, "MyN3wP@ssw0rd");
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Ошибка установки пароля'.");
            }
        }
        
        _mapper.Map(Input, user);

        var updateProfileResult = await _userManager.UpdateAsync(user);
        if (!updateProfileResult.Succeeded)
        {
            throw new InvalidOperationException($"Ошибка обновления данных о пользователе'.");
        }

        if (Input.PhoneNumber != user.PhoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            if (!setPhoneResult.Succeeded)
            {
                throw new InvalidOperationException($"Ошибка обновления телефона'.");
            }
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Пользователь обновлен";
        return Page();
    }
}
