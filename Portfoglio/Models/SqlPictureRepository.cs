using System.Collections.Generic;
using System.Threading.Tasks;
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
            return db.Pictures.Include(a => a.Album);
        }

        public async Task<Picture> GetItem(int id)
        {
            return await db.Pictures.Include(a => a.Album).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async void Create(Picture item)
        {
            await db.Pictures.AddAsync(item);
        }

        public async void Create(List<Picture> items)
        {
            await db.Pictures.AddRangeAsync(items);
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

        public void Show(int id)
        {
            var item = db.Pictures.Find(id);
            item.State = true;
            Update(item);
        }

        public void Show(Picture item)
        {
            item.State = true;
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

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}