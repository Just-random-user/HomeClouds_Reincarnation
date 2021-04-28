using Clouds.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


#nullable enable

namespace Clouds.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CloudsDbContext db;
        public HomeController(CloudsDbContext context)
        {
            db = context;
        }
        public IActionResult FileExplorer(string? path = null)
        {

            ViewData["Paths"] = SupportClasses.Path.GetPaths(path);

            return View("FileExplorer");
        }
    }
}
