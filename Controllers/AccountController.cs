using Clouds.Data;
using Clouds.Models;
using Clouds.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clouds.Controllers
{
    public class AccountController : Controller
    {
        private CloudsDbContext db;

        public AccountController(CloudsDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault<User>(u => u.Login == model.Login && u.Password == model.Password);
                if(user != null)
                {
                    await Authenticate(model.Login);
                    return RedirectToAction("FileExplorer", "Home");
                }
                ModelState.AddModelError("", "Login or password incorrect");
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(u => u.Login == model.Login);
                if (user == null)
                {
                    db.Users.Add(new User { Login = model.Login, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Login);

                    return RedirectToAction("FileExplorer", "Home");
                }
                else
                    ModelState.AddModelError("", "Login or password incorrect");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
