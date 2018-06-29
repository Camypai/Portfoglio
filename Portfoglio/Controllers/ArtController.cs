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
        private IRepository<Album> albumFromDB = new SqlAlbumRepository();
        
        public IActionResult Index()
        {
            return View();
        }
//
//        public IActionResult About()
//        {
//            ViewData["Message"] = "Your application description page.";
//
//            return View();
//        }
//
//        public IActionResult Contact()
//        {
//            ViewData["Message"] = "Your contact page.";
//
//            return View();
//        }
//
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
//        }
    }
}