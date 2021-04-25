﻿using Clouds.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;


namespace Clouds.Controllers
{
    public class FileController : Controller
    {
        [HttpPost]
        public IActionResult Upload(FileViewModel file)
        {
            if (file != null && file.Upload != null)
            {
                string fileName = Path.GetFileName(file.Upload.FileName);
                using var fileStream =
                   new FileStream(file.Path + Path.DirectorySeparatorChar + fileName, FileMode.Create);
                file.Upload.CopyToAsync(fileStream);
            }
            return RedirectToAction("FileExplorer", "Home");
        }
        [HttpPost]
        public IActionResult Download(FileViewModel file)
        {
            string path = file.Path;
            new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);
            return PhysicalFile(path, contentType, Path.GetFileName(path));
        }
    }
}