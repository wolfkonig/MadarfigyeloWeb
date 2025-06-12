using MadarfigyeloWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MadarfigyeloWeb.Data
{
    public class MvcTestContext : DbContext
    {
        public MvcTestContext(DbContextOptions<MvcTestContext> options) : base(options) { }

        public DbSet<DbTest> Test { get; set; }
    }
}
