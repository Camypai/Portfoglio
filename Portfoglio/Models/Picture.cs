namespace Portfoglio.Models
{
    public class Picture : BaseModel
    {
        public int  Sorted { get; set; }
        public Album Album { get; set; }
    }
}