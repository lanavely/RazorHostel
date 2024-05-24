using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Auto.Models;

public class UserModel
{
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateOnly? Birthdate { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Никнейм")]
        public string UserName { get; set; }

        [DisplayName("Фамилия Имя Отчество")]
        public string FullName => $"{LastName} {FirstName} {Patronymic}";
}