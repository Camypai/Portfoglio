using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Portfoglio.Models
{
    public class SqlAlbumRepository : IRepository<Album>
    {
        private readonly Context db;

        public SqlAlbumRepository(Context context)
        {
            db = context;
        }

        public IEnumerable<Album> GetList()
        {
            return db.Albums;
        }

        public Album GetItem(int id)
        {
            return db.Albums.Find(id);
        }

        public void Create(Album item)
        {
            db.Albums.Add(item);
        }

        public void Update(Album item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Hide(int id)
        {
            var item = db.Albums.Find(id);
            item.State = false;
            Update(item);
        }

        public void Hide(Album item)
        {
            item.State = false;
            Update(item);
        }
        
        public void Delete(int id)
        {
            var item = db.Albums.Find(id);
            Delete(item);
        }

        public void Delete(Album item)
        {
            if (item != null)
                db.Albums.Remove(item);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}