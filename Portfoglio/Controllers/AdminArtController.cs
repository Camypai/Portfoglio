using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private IRepository<Picture> dbPicture;
        private IHostingEnvironment _environment;

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

            var pictures = (from picture in album.Pictures
                let path = $"/images/{picture.FileName}"
                select new Picture
                {
                    Album = _album,
                    Name = picture.FileName,
                    State = true,
                    Path = path
                }).ToList();

            _album.Pictures = pictures;
            _album.State = album.State;
            _album.Description = album.Description;
            _album.Name = album.Name;

            dbAlbum.Update(_album);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPictures(int id, IFormFileCollection pictures)
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
            
//            _album.Pictures.AddRange(_pictures);
            dbPicture.Create(_pictures);
            await dbPicture.SaveAsync();
            
            return RedirectToAction("ShowEditAlbum", _album);
        }

        public async void EditPicture(int id, Method method)
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }

            await dbPicture.SaveAsync();
        }
    }
}