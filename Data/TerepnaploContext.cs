using MadarfigyeloWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MadarfigyeloWeb.Data
{
    public class TerepnaploContext : IdentityDbContext<ApplicationUser>
    {
        public TerepnaploContext(DbContextOptions<TerepnaploContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Odutelep> Odutelep { get; set; }
        public DbSet<Odu> Odu { get; set; }
        public DbSet<Latogatas> Latogatas { get; set; }
    }
}
