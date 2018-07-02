using System.Threading.Tasks;
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
            return View();
        }

        public IActionResult _CreateAlbum()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum(Album album)
        {
            dbAlbum.Create(album);
            await dbAlbum.SaveAsync();
            return RedirectToAction("Index");
        }
    }
}