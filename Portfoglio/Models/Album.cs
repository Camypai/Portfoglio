using System.Collections.Generic;

namespace Portfoglio.Models
{
    public class Album : BaseModel, IAlbum
    {
        public string Description { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }
}