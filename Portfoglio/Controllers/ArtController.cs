using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfoglio.Models;

namespace Portfoglio.Controllers
{
    public class ArtController : Controller
    {
        private readonly SqlContext db;

        public ArtController(Context context)
        {
            db = new SqlContext(context);
        }
        
        public IActionResult Index()
        {
            var albums = db.AlbumRepository.GetList().Where(a => a.State);
            albums = albums.Select(a => new Album
            {
                Id = a.Id,
                Description = a.Description,
                Name = a.Name,
                Pictures = a.Pictures.Where(p=>p.AlbumId ==  a.Id & p.State).ToList(),
                State = a.State
            });
            
            return View(albums);
        }

        public IActionResult About()
        {
            return View();
        }
        
        public IActionResult Pricelist()
        {
            return View();
        }
//
//        public IActionResult Contact()
//        {
//            ViewData["Message"] = "Your contact page.";
//
//            return View();
//        }
    }
}