using AuthTest.Data;
using AuthTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuthTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CloudsDbContext db;
        public HomeController(CloudsDbContext context)
        {
            db = context;
        }
        public IActionResult FileExplorer()
        {
            var user = db.Users.FirstOrDefault<User>(u => u.Login == User.Identity.Name);
            return View("FileExplorer", user);
        }
    }
}
