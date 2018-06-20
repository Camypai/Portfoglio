namespace Portfoglio.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }
    }
}