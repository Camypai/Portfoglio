using System.ComponentModel.DataAnnotations;

namespace Portfoglio.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Not allow Login")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Not allow password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}