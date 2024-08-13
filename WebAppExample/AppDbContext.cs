using Microsoft.EntityFrameworkCore;

namespace WebAppExample
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //DbSet: Represents a collection of entities that can be queried from the database.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
