namespace Portfoglio.Models
{
    public class Picture : BaseModel
    {
        public string Path { get; set; }
        public int  Sorted { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}