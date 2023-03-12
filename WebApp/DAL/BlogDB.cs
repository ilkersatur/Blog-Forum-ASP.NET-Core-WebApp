using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.Config;

namespace WebApp.DAL
{
    //public class KutuphaneDB:IdentityDbContext
    public class BlogDB:IdentityDbContext<Uye,Rol,int>
    {
        public BlogDB(DbContextOptions<BlogDB> options):base(options)
        {
                
        }

        public DbSet<Konu> Konular { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Yazar> Yazarlar { get; set; }
        public DbSet<Konu_Kategori> Konu_Kategori { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Rol> Roller { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<Konu>(new KonuCFG());
            builder.ApplyConfiguration<Rol>(new RolCFG());

            base.OnModelCreating(builder);
        }
    }
}
