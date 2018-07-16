using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfoglio.Models;

namespace Portfoglio.Controllers
{
//    [Authorize]
    public class AdminArtController : Controller
    {
        private readonly SqlContext db;
        private readonly IHostingEnvironment _environment;

        public AdminArtController(Context context, IHostingEnvironment environment)
        {
            db = new SqlContext(context);
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult About()
        {
            const string path = @"./Views/Art/_About.cshtml";
            var result = GetTextFromFile(path);
            
            return View(result);
        }
        
        public IActionResult Pricelist()
        {
            const string path = @"./Views/Art/_Pricelist.cshtml";
            var result = GetTextFromFile(path);
            
            return View(result);
        }

        private static HtmlTextModel GetTextFromFile(string path)
        {
            var result = new HtmlTextModel{View = string.Empty};
            
            using (var sr = new StreamReader(path))
            {
                result.View = sr.ReadToEnd();
            }

            return result;
        }
        
        [HttpPost]
        public async Task HtmlEdit(string data, string target)
        {
            string path;
            
            switch (target)
            {
                    case "pricelistEdit":
                        path = @"./Views/Art/_Pricelist.cshtml";
                        break;
                    case "aboutEdit":
                        path = @"./Views/Art/_About.cshtml";
                        break;
                    default:
                        return;
            }
            
            
            using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                await sw.WriteAsync(data);
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> CreateAlbum(AlbumViewModel album)
        {
            
            db.AlbumRepository.Create(album.ToAlbum());
            await db.AlbumRepository.SaveAsync();
            var _album = db.AlbumRepository
                .GetList()
                .FirstOrDefault(a => a.Name == album.Name & a.Description == album.Description & !a.Pictures.Any());

            if (!(album.Pictures != null & _album != null)) return RedirectToAction("Index");
            var _pictures = await SavePicturesOnServerAndBuild(_album.Id, album.Pictures);
            db.PictureRepository.Create(_pictures);
            await db.PictureRepository.SaveAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowEditAlbum(int id)
        {
            var album = await db.AlbumRepository.GetItem(id);
            return View(album);
        }

        [HttpPost]
        public async Task<IActionResult> EditAlbum(AlbumViewModel album)
        {
            var _album = await db.AlbumRepository.GetItem(album.Id);

            if (album.Pictures != null)
            {
                var _pictures = await SavePicturesOnServerAndBuild(album.Id, album.Pictures);
                db.PictureRepository.Create(_pictures);
                await db.PictureRepository.SaveAsync();
            }

            if(!Equals(_album.State, album.State))
            _album.State = album.State;
            
            if(!Equals(_album.Description,album.Description))
            _album.Description = album.Description;
            
            if(!Equals(_album.Name,album.Name))
            _album.Name = album.Name;

            db.AlbumRepository.Update(_album);
            await db.AlbumRepository.SaveAsync();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAlbum(Album album)
        {
            db.AlbumRepository.Delete(album);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Save pictures on server, and write them on db
        /// </summary>
        /// <param name="id">Album id</param>
        /// <param name="pictures">Pictures from web form</param>
        /// <returns>IEnumerable IPicture</returns>
        private async Task<IEnumerable<Picture>> SavePicturesOnServerAndBuild(int id, IFormFileCollection pictures)
        {
            var _album = await db.AlbumRepository.GetItem(id);

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
            var picture = await db.PictureRepository.GetItem(id);

            switch (method)
            {
                case Method.Edit:
                    break;
                case Method.Hide:
                    db.PictureRepository.Hide(picture);
                    break;
                case Method.Delete:
                    db.PictureRepository.Delete(picture);
                    break;
                case Method.Show:
                    db.PictureRepository.Show(picture);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }

            await db.PictureRepository.SaveAsync();

            return RedirectToAction("ShowEditAlbum", new { id = picture.Album.Id});
        }
    }
}