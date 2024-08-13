using System;
using System.Collections.Generic;
using System.Web.UI;
using WebGrease.Css.Extensions;

namespace WebFormExample
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // Initial data binding
                    BindGridView();
                }
                else
                {
                    // Restore GridView data from ViewState
                    if (ViewState["Products"] != null)
                    {
                        GridView1.DataSource = (List<Product>)ViewState["Products"];
                        GridView1.DataBind();
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Simulate a postback
        }

        private void BindGridView()
        {
            //List<Product> products = new List<Product>
            //{
            //    new Product { Id = 1, ProductName = "Product 1", Price = 10.00m },
            //    new Product { Id = 2, ProductName = "Product 2", Price = 20.00m },
            //    new Product { Id = 3, ProductName = "Product 3", Price = 30.00m }
            //};
            ////foreach (var product in products)
            ////{
            ////    product. = 0; // or default(int)
            ////}
            //// Store the products in ViewState
            //ViewState["Products"] = products;

            //GridView1.DataSource = (List<Product>)products;
            //GridView1.DataBind();
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}