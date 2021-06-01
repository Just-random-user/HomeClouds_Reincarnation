using System;
using System.Linq;
using System.Security.Claims;
using Clouds.Data;
using Clouds.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Path = System.IO.Path;


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

        [HttpPost]
        public IActionResult Share(string path)
        {
            var url = $"https://{Request.Host}/File/SharedDownload?guid=";
            var link = LinkGenerator.GetDownloadLink(User.Identity.Name, path, db);
            if (link == null)
                return BadRequest();
            return Json(new {status = "success", link = url + link});
        }
    }
}