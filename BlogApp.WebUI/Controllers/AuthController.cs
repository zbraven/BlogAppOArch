using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using BlogApp.WebUI.Authorize;
using BlogApp.Business;

namespace BlogApp.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthorBusiness _authorBusiness;

        public AuthController(AuthorBusiness _authorBusiness)
        {
            this._authorBusiness = _authorBusiness;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var loginResult = _authorBusiness.Login(username,password);
            if (!loginResult.IsSucceed)
                return Json(new { isSucceed = loginResult.IsSucceed, message = loginResult.Message, errors = loginResult.Errors, instance = "" });

            var user = loginResult.Instance;

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("FullName", $"{user.Fullname}"),
                        new Claim(ClaimTypes.Role, "Administrator"),
                    };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.UtcNow,
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1200),
                AllowRefresh = true,
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Json(new { isSucceed = loginResult.IsSucceed, message = loginResult.Message, errors = loginResult.Errors, redirect = "/Home/Index" });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Login","Auth");
        }
    }
}
