using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Portfoglio.Models
{
    public class SqlUserRepository : IRepository<User>
    {
        private readonly Context db;

        public SqlUserRepository(Context context)
        {
            db = context;
        }

        public async Task<User> GetUser(User user)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Name == user.Name & u.Password == user.Password & u.State);
        }

        public IEnumerable<User> GetList()
        {
            return db.Users;
        }

        public async Task<User> GetItem(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async void Create(User item)
        {
            await db.Users.AddAsync(item);
        }

        public async void Create(IEnumerable<User> items)
        {
            await db.Users.AddRangeAsync(items);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = db.Users.Find(id);
            Delete(item);
        }

        public void Delete(User item)
        {
            if (item != null)
            {
                db.Users.Remove(item);
            }
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