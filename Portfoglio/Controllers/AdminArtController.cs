using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfoglio.Models;

namespace Portfoglio.Controllers
{
    public class AdminArtController : Controller
    {
        private readonly IRepository<Album> dbAlbum;
        private readonly IRepository<Picture> dbPicture;
        private readonly IHostingEnvironment _environment;

        public AdminArtController(Context context, IHostingEnvironment environment)
        {
            dbAlbum = new SqlAlbumRepository(context);
            dbPicture = new SqlPictureRepository(context);
            _environment = environment;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateAlbum(Album album)
        {
            dbAlbum.Create(album);
            await dbAlbum.SaveAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowEditAlbum(int id)
        {
            var album = await dbAlbum.GetItem(id);
            return View(album);
        }

        [HttpPost]
        public async Task<IActionResult> EditAlbum(AlbumViewModel album)
        {
            var _album = await dbAlbum.GetItem(album.Id);

            if (album.Pictures != null)
            {
                var _pictures = await SavePicturesOnServerAndBuild(album.Id, album.Pictures);
                dbPicture.Create(_pictures);
                await dbPicture.SaveAsync();
            }

            if(!Equals(_album.State, album.State))
            _album.State = album.State;
            
            if(!Equals(_album.Description,album.Description))
            _album.Description = album.Description;
            
            if(!Equals(_album.Name,album.Name))
            _album.Name = album.Name;

            dbAlbum.Update(_album);
            await dbAlbum.SaveAsync();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAlbum(Album album)
        {
            dbAlbum.Delete(album);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Save pictures on server, and write them on db
        /// </summary>
        /// <param name="id">Album id</param>
        /// <param name="pictures">Pictures from web form</param>
        /// <returns>IEnumerable<Picture></returns>
        private async Task<IEnumerable<Picture>> SavePicturesOnServerAndBuild(int id, IFormFileCollection pictures)
        {
            var _album = await dbAlbum.GetItem(id);

            var _pictures = new List<Picture>();
            foreach (var picture in pictures)
            {
                var path = $"/images/{picture.FileName}";
                _pictures.Add(new Picture
                {
                    Album = _album,
                    Name = picture.FileName,
                    State = true,
                    Path = path
                });

                using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }
            }

            return _pictures;
        }

        public async Task<IActionResult> EditPicture(int id, Method method)
        {
            var picture = await dbPicture.GetItem(id);

            switch (method)
            {
                case Method.Edit:
                    break;
                case Method.Hide:
                    dbPicture.Hide(picture);
                    break;
                case Method.Delete:
                    dbPicture.Delete(picture);
                    break;
                case Method.Show:
                    dbPicture.Show(picture);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }

            await dbPicture.SaveAsync();

            return RedirectToAction("ShowEditAlbum", new { id = picture.Album.Id});
        }
    }
}