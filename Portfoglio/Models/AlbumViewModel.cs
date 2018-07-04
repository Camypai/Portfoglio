using Microsoft.AspNetCore.Http;

namespace Portfoglio.Models
{
    public class AlbumViewModel : BaseModel
    {
        public string Description { get; set; }
        public IFormFileCollection Pictures { get; set; }
    }
}