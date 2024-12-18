using Microsoft.AspNetCore.Mvc;
using Microsoft.SharePoint.Client;

namespace WebAppExample.Controllers
{
    public class SharePointController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
