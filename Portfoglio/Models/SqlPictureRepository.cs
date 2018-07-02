using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Portfoglio.Models
{
    public class SqlPictureRepository : IRepository<Picture>
    {
        private readonly Context db;

        public SqlPictureRepository(Context context)
        {
            db = context;
        }
        
        public IEnumerable<Picture> GetList()
        {
            return db.Pictures;
        }

        public Picture GetItem(int id)
        {
            return db.Pictures.Find(id);
        }

        public void Create(Picture item)
        {
            db.Pictures.Add(item);
        }

        public void Update(Picture item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Hide(int id)
        {
            var item = db.Pictures.Find(id);
            item.State = false;
            Update(item);
        }

        public void Hide(Picture item)
        {
            item.State = false;
            Update(item);
        }

        public void Delete(int id)
        {
            var item = db.Pictures.Find(id);
            Delete(item);
        }

        public void Delete(Picture item)
        {
            if (item != null)
                db.Pictures.Remove(item);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}