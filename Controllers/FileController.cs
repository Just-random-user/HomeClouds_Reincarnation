using System;
using Clouds.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Claims;
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
                if (Directory.Exists(path))
                {
                    var tmpFileName = Path.GetRandomFileName();
                    tmpFileName = tmpFileName.Replace(Path.GetExtension(tmpFileName), ".zip");
                    var zipPath = Path.GetPathRoot(path) + tmpFileName;
                    ZipFile.CreateFromDirectory(path, zipPath);
                    path = zipPath;
                    var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None,
                        (int)(new FileInfo(path)).Length, FileOptions.DeleteOnClose);
                    return File(
                        stream, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path));
                }
                else
                {
                    var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None,
                        (int)(new FileInfo(path)).Length, FileOptions.DeleteOnClose);
                    return File(
                        stream, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path));  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect($"/?path={Path.GetDirectoryName(path)}");
            }
            finally
            {
                //System.IO.File.Delete(path);
            }
        }

        public IActionResult SharedDownload(string guid)
        {
            try
            {
                string path = _db.SharedFiles.FirstOrDefault(f => f.Link == guid)?.FilePath;
                if (Directory.Exists(path))
                {
                    var tmpFileName = Path.GetRandomFileName();
                    tmpFileName = tmpFileName.Replace(Path.GetExtension(tmpFileName), ".zip");
                    var zipPath = Path.GetPathRoot(path) + tmpFileName;
                    ZipFile.CreateFromDirectory(path, zipPath);
                    path = zipPath;
                    var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None,
                        (int)(new FileInfo(path)).Length, FileOptions.DeleteOnClose);
                    return File(
                        stream, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path));
                }
                else
                {
                    var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None,
                        (int)(new FileInfo(path)).Length, FileOptions.DeleteOnClose);
                    return File(
                        stream, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(path));  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        public IActionResult Delete(string path)
        {
            try
            {
                System.IO.File.Delete(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hello world!");
            }
            return Redirect( $"/?path={Path.GetDirectoryName(path)}") ;
        }
        
    }
}
