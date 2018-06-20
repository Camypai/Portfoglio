using Microsoft.AspNetCore.Mvc;
using Portfoglio.Models;

namespace Portfoglio.Controllers
{
    public class AdminArtController : Controller
    {
        private IRepository<Album> dbAlbum = new SqlAlbumRepository();
        private IRepository<Picture> dbPicture = new SqlPictureRepository();
        
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}