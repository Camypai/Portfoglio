namespace Portfoglio.Models
{
    public interface IPicture : IBaseModel
    {
        string Path { get; set; }
        int  Sorted { get; set; }
        int AlbumId { get; set; }
        Album Album { get; set; }
    }
}