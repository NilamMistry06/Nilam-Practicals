using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace WebAppExample
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base(@"Server=NILU\\SQLEXPRESS;Database=NewDB;Trusted_Connection=True;")
        {
        }
    }
}
