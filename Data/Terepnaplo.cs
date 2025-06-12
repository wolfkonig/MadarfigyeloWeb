using MadarfigyeloWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MadarfigyeloWeb.Data
{
    public class Terepnaplo : DbContext
    {
        public Terepnaplo(DbContextOptions<Terepnaplo> options) : base(options) { }

        public DbSet<Odutelep> Odutelep { get; set; }
        public DbSet<Odu> Odu { get; set; }
        public DbSet<Latogatas> Latogatas { get; set; }
    }
}
