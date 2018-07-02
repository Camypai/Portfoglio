using Microsoft.EntityFrameworkCore;

namespace Portfoglio.Models
{
    public sealed class Context : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Server=(localdb)\\artdb;Database=artdb;Trusted_Connection=True;");
//        }
    }
}