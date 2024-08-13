using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace WebAppExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productRepository.GetProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var createdProduct = await _productRepository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productRepository.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }


        public void UserProcedureAndFunctionUsingEF()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("DefaultConnection");

            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                // Execute stored procedure to add a new product
                var name = "Tablet";
                var price = 299.99m;
                var categoryId = 1;
                context.Database.ExecuteSqlRaw("EXEC AddProduct @p0, @p1, @p2", name, price, categoryId);
                Console.WriteLine("Product added successfully.");

                // Execute user-defined function to get total products by category
                var totalProducts = context.Products
                    .FromSqlRaw("SELECT dbo.GetTotalProductsByCategory(@p0) AS TotalProducts", categoryId)
                    .AsEnumerable()
                    .FirstOrDefault();
                Console.WriteLine($"Total products in category {categoryId}: {totalProducts}");
            }
        }
        public void UserProcedureAndFunctionWithoutEF()
        {
            string connectionString = new ConfigurationManager().GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Execute stored procedure to add a new product
                using (SqlCommand command = new SqlCommand("AddProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100)).Value = "Tablet";
                    command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal)).Value = 299.99m;
                    command.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int)).Value = 1;

                    command.ExecuteNonQuery();
                    Console.WriteLine("Product added successfully.");
                }

                // Execute user-defined function to get total products by category
                using (SqlCommand command = new SqlCommand("SELECT dbo.GetTotalProductsByCategory(@CategoryId)", connection))
                {
                    command.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int)).Value = 1;

                    int totalProducts = (int)command.ExecuteScalar();
                    Console.WriteLine($"Total products in category 1: {totalProducts}");
                }
            }
        }
    }
}
