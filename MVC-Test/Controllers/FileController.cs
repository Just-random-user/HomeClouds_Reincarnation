using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using MVC_Test.ViewModels;
using System;

namespace MVC_Test.Controllers
{
    public class FileController : Controller
    {
        [HttpGet]
        public IActionResult FileExplorer(FileViewModel file)
        {
            if (String.IsNullOrEmpty(file.path))
            {
                file = new FileViewModel("D:\\");
            }

            return View("Index", file);
        }

        [HttpPost]
        public IActionResult Download(FileViewModel file)
        {
            string path = file.path;
            string name = Path.GetFileName(path);
            new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);
            return PhysicalFile(path, contentType, name);
        }
    }
}