using Microsoft.AspNetCore.Mvc;
using Portfoglio.Models;

namespace Portfoglio.Controllers
{
    public class AdminArtController : Controller
    {
        private readonly IRepository<Album> dbAlbum;
        private IRepository<Picture> dbPicture;

        public AdminArtController(Context context)
        {
            dbAlbum = new SqlAlbumRepository(context);
            dbPicture = new SqlPictureRepository(context);
        }
        
        // GET
        public IActionResult Index()
        {
            var album = dbAlbum.GetList();
            return View(album);
        }
    }
}