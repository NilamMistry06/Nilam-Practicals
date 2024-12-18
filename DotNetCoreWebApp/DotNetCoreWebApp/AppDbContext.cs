using DotNetCoreWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebApp
{
    public class AppDbContext: DbContext
    {
        public DbSet<Product> Product { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options)
        {
        }
    }
}
