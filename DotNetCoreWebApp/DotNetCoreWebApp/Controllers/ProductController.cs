using DotNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("NewDB");

            var allProducts = new List<Product>();
            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                var products = new Product { Id = 7, Name = "Mouse", Price = 120.00M, CategoryId = 1 };
                context.Product.Add(products);
                context.SaveChanges();

                foreach (var p in context.Product.ToList())
                {
                    allProducts.Add(p);
                }
            }
            return View(allProducts);
        }
    }
}
