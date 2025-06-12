using MadarfigyeloWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MadarfigyeloWeb.Data
{
    public class TerepnaploContext : DbContext
    {
        public TerepnaploContext(DbContextOptions<TerepnaploContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Odu>().Property(o => o.GpsLatitude).HasPrecision(8, 6);
            modelBuilder.Entity<Odu>().Property(o => o.GpsLongitude).HasPrecision(9, 6);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Odutelep> Odutelep { get; set; }
        public DbSet<Odu> Odu { get; set; }
        public DbSet<Latogatas> Latogatas { get; set; }
    }
}
