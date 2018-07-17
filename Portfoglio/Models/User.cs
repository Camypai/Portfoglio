namespace Portfoglio.Models
{
    public class User : BaseModel
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}