namespace Portfoglio.Models
{
    public interface IBaseModel
    {
        int Id { get; set; }
        string Name { get; set; }
        bool State { get; set; }
    }
}