using System.ComponentModel.DataAnnotations;

namespace Portfoglio.ViewModel
{
    public class RegistrationModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "Email does not entry")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}