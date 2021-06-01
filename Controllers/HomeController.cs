using System;
using System.IO;
using Clouds.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
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
            try
            {
                ViewData["Paths"] = SupportClasses.Path.GetPaths(path);
                ViewData["Path"] = path;
                return View("FileExplorer");
            }
            catch (Exception e)
            {
                ViewData["Paths"] = SupportClasses.Path.GetPaths(path);
                ViewData["Path"] = path;
                return Redirect($"/?path={Path.GetDirectoryName(path)}");
            }

        }
    }
}
