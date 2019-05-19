using huncho.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace huncho.Data
{
    public class HunchoDbContext : DbContext
    {
        public HunchoDbContext(DbContextOptions<HunchoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
