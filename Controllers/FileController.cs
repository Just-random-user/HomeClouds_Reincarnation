using System;
using Clouds.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Clouds.Data;

#nullable enable

namespace Clouds.Controllers
{
    public class FileController : Controller
    {
        
        private CloudsDbContext _db;
        public FileController(CloudsDbContext context)
        {
            _db = context;
        }
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(string path, IFormFile file)
        {
            try
            {
                if (file != null && path != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    await using var fileStream = new FileStream(path + Path.DirectorySeparatorChar + fileName, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }
            
                return Redirect( $"/?path={Path.GetDirectoryName(path)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect($"/?path={Path.GetDirectoryName(path)}");
            }

        }
        
        public IActionResult Download(string path)
        {
            try
            {
                new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);
                return PhysicalFile(path, contentType, Path.GetFileName(path));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect($"/?path={Path.GetDirectoryName(path)}");
            }
        }
        
        [HttpPost]
        public IActionResult Share(string path)
        {   
            try
            {
                string path = _db.SharedFiles.FirstOrDefault(f => f.Id == file)?.FilePath;
                new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);
                return PhysicalFile(path, contentType, Path.GetFileName(path));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("FileExplorer", "Home");
            }
        } 
        
        public IActionResult SharedDownload(string file)
        {   
            try
            {
                string path = _db.SharedFiles.FirstOrDefault(f => f.Id == file)?.FilePath;
                new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);
                return PhysicalFile(path, contentType, Path.GetFileName(path));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("FileExplorer", "Home");
            }
        }

        public IActionResult Delete(string path)
        {
            
            return Redirect( $"/?path={Path.GetDirectoryName(path)}") ;
        }
        
    }
}
