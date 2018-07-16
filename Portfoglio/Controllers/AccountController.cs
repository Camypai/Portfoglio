using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Portfoglio.Models;
using Portfoglio.ViewModel;

namespace Portfoglio.Controllers
{
    public class AccountController : Controller
    {
        private readonly SqlContext db;

        public AccountController(Context context)
        {
            db = new SqlContext(context);
        }

        // GET
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await db.AuthRepository.GetUser(new User
            {
                Password = Crypto.GetHashString(model.Password),
                Name = model.Name
            });

            if (user == null)
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            else
            {
                await Authenticate(model.Name);

                return RedirectToAction("Index", "AdminArt");
            }

            return View(model);
        }

        private async Task Authenticate(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}