using Microsoft.AspNetCore.Mvc;
using BlogApp.WebUI.Helpers;
using BlogApp.WebUI.Authorize;
using BlogApp.WebUI.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using BlogApp.Business;

namespace BlogApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AuthorBusiness authorBusiness;

        public HomeController(IWebHostEnvironment environment, AuthorBusiness authorBusiness)
        {
            _environment = environment;
            this.authorBusiness = authorBusiness;
        }
        public IActionResult Index()
        {
            var datas = authorBusiness.GetAll();
            return View();
        }
    }
}
