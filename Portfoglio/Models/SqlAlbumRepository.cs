using System.Collections.Generic;
using System.Threading.Tasks;
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
            return db.Albums.Include(p => p.Pictures);
        }

        public async Task<Album> GetItem(int id)
        {
            return await db.Albums.Include(p => p.Pictures).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async void Create(Album item)
        {
            item.State = true;
            await db.Albums.AddAsync(item);
        }

        public async void Create(List<Album> items)
        {
            foreach (var album in items)
            {
                album.State = true;
            }
            await db.Albums.AddRangeAsync(items);
        }

        public void Update(Album item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Hide(int id)
        {
            var item = db.Albums.Find(id);
            item.State = !item.State;
            Update(item);
        }

        public void Hide(Album item)
        {
            item.State = !item.State;
            Update(item);
        }

        public void Show(int id)
        {
            var item = db.Albums.Find(id);
            item.State = true;
            Update(item);
        }

        public void Show(Album item)
        {
            item.State = true;
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

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}