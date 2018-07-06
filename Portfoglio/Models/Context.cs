using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Portfoglio.Models
{
    public sealed class Context : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
            if (!((RelationalDatabaseCreator) Database.GetService<IDatabaseCreator>()).Exists())
            {
                Database.Migrate();
            }
        }
    }
}