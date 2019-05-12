using huncho.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace huncho.Data
{
    public class HunchoDbContext : IdentityDbContext
    {
        public HunchoDbContext(DbContextOptions<HunchoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
