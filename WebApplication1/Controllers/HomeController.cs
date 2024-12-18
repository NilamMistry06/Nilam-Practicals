using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    using System;

    
    public class HomeController : Controller
    {
        public static double Average(int a, int b)
        {
            return a + b / 2;
        }

        public ActionResult Index()
        {
            Console.WriteLine(Average(2, 1));
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}