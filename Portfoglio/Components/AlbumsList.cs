using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfoglio.Models;

namespace Portfoglio.Components
{
    public class AlbumsList : ViewComponent
    {
        private readonly SqlAlbumRepository dbAlbumRepository;

        public AlbumsList(Context context)
        {
            dbAlbumRepository = new SqlAlbumRepository(context);
        }

        public IViewComponentResult Invoke()
        {
            return View(dbAlbumRepository.GetList());
        }
    }
}