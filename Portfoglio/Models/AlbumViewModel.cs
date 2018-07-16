using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Portfoglio.Models
{
    public class AlbumViewModel : BaseModel
    {
        public string Description { get; set; }
        public IFormFileCollection Pictures { get; set; }

        public Album ToAlbum()
        {
            return new Album
            {
                Id = Id,
                Name = Name,
                Description = Description,
                State = State
            };
        }
    }
}