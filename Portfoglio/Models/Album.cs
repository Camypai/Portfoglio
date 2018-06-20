using System.Collections.Generic;

namespace Portfoglio.Models
{
    public class Album : BaseModel
    {
        public string Description { get; set; }
        public List<Picture> Pictures { get; set; }
    }
}