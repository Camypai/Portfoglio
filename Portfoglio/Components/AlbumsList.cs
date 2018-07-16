using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfoglio.Models;

namespace Portfoglio.Components
{
    public class AlbumsList : ViewComponent
    {
        private readonly SqlContext db;

        public AlbumsList(Context context)
        {
            db = new SqlContext(context);
        }

        public IViewComponentResult Invoke()
        {
            return View(db.AlbumRepository.GetList());
        }
    }
}