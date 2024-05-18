using System.ComponentModel.DataAnnotations;

namespace Auto.Models;

public class UserLoginModel
{
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Никнейм")]
    public string UserName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Display(Name = "запомнить")]
    public bool RememberMe { get; set; } = false;
}