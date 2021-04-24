using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using MVC_Test.Models;

namespace MVC_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Upload(IFormFile file)
        {
            if(file!=null)
            {
                string fileName = Path.GetFileName(file.FileName);
                using var fileStream =
                    new FileStream(_environment.WebRootPath + 
                                   Path.PathSeparator + "files" + Path.PathSeparator + fileName, FileMode.Create);
                file.CopyToAsync(fileStream);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Download(string path)
        {
            string name = Path.GetFileName(path);
            new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);
            return PhysicalFile(path, contentType, name);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}