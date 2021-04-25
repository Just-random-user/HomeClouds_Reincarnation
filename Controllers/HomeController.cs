using Clouds.Data;
using Clouds.Models;
using Clouds.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
        public IActionResult FileExplorer(FileViewModel file)
        {
            if (String.IsNullOrEmpty(file.Path))
            {
                file = new FileViewModel("C:\\", User.Identity.Name);//тута вставь свой путь C: нужно добавить отображение дисков для root
            }

            return View("FileExplorer", file);
        }
    }
}
