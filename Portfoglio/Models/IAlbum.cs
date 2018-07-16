using System.Collections.Generic;

namespace Portfoglio.Models
{
    public interface IAlbum : IBaseModel
    {
        string Description { get; set; }
        IEnumerable<Picture> Pictures { get; set; }
        
    }
}