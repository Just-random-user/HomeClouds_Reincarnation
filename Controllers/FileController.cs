using Clouds.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Threading.Tasks;

#nullable enable

namespace Clouds.Controllers
{
    public class FileController : Controller
    {
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(string path, IFormFile file)
        {
            if (file != null && path != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                using var fileStream =
                   new FileStream(path + Path.DirectorySeparatorChar + fileName, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
            return RedirectToAction("FileExplorer", "Home", path);
        }
        public IActionResult Download(string path)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);
            if(contentType == null || path == null)
                return RedirectToAction("FileExplorer", "Home");
            return PhysicalFile(path, contentType, Path.GetFileName(path));
        }
    }
}
