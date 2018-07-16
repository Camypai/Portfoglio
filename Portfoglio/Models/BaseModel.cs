namespace Portfoglio.Models
{
    public class BaseModel : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }
    }
}