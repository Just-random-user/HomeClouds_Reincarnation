using Clouds.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Clouds.Components
{
    public class FileViewComponent : ViewComponent
    {
        private readonly IWebHostEnvironment _env;

        public FileViewComponent(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IViewComponentResult Invoke(FileViewModel model)
        {
            model.Icon = SupportClasses.Icons.IconPath(model.Path, _env.WebRootPath);
            model.Name = Path.GetFileNameWithoutExtension(model.Path);
            if (string.IsNullOrEmpty(model.Name))
            {
                model.Name = model.Path;
            }
            if (Directory.Exists(model.Path))
            {
                model.IsDir = true;
            }
            else
                model.IsDir = false;
            
            return View("~/Views/Home/Components/File/File.cshtml", model);
        }
    }
}
