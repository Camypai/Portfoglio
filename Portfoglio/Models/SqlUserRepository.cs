using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Portfoglio.Models
{
    public class SqlUserRepository : IRepositoryAuth
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
    }
}