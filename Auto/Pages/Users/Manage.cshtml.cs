using Auto.Data;
using Auto.Data.Entities;
using Auto.Helpers;
using Auto.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Auto.Pages.Users;

[Authorize(Roles = Consts.Admin)]
public class ManageModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ManageModel(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext context,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public AdminUserEditModel Input { get; set; }

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        if (id is null)
        {
            return NotFound("Не указан id пользователя");
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound($"Не найден пользователь '{_userManager.GetUserId(User)}'.");
        }

        Input = _mapper.Map<AdminUserEditModel>(user);
        var roles = _roleManager.Roles.Select(r => new
        {
            r.Id,
            r.Name,
            DisplayName = RoleNameHelper.RoleToString(r.Name)
        });
        
        ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "Name");
        ViewData["RoleName"] = new SelectList(roles, "Name", "DisplayName");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var user = await _userManager.FindByIdAsync(Input.Id);
        if (user == null)
        {
            return NotFound("Не найден пользователь");
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

        if (Input.RoleName is not null)
        {
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            await _userManager.AddToRoleAsync(user, Input.RoleName);
            await _userManager.UpdateAsync(user);
        }

        StatusMessage = "Пользователь обновлен";
        return RedirectToPage("./Index");
    }
}
