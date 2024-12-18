using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAppExample.Controllers
{
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Dummy check: In a real application, validate username and password against a database
            if (username == "admin" || password == "password") {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user with the created principal
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme ,principal);
                return RedirectToAction("FindLocation", "Location");
            }
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
