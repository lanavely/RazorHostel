using System.ComponentModel.DataAnnotations;

namespace Auto.Models;

public class UserEditModel : UserModel
{
    [StringLength(100, ErrorMessage = "{0} должен содержать от {2} до {1} символов", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string? ConfirmPassword { get; set; }
}