using Microsoft.AspNetCore.Mvc;
using BlogApp.WebUI.Helpers;
using BlogApp.WebUI.Authorize;
using BlogApp.WebUI.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using BlogApp.Business;
using BlogApp.Model;

namespace BlogApp.WebUI.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AuthorBusiness authorBusiness;

        public AuthorController(IWebHostEnvironment environment, AuthorBusiness authorBusiness)
        {
            _environment = environment;
            this.authorBusiness = authorBusiness;
        }
        public IActionResult Index()
        {
            var datas = authorBusiness.GetAll();
            return View(datas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var datas = new Author();
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author model)
        {
            var response = await authorBusiness.Create(model);
            if (response.IsSucceed)
            {
                return RedirectToAction("Index","Author");
            }

            ViewBag.Error = response.Message;
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var datas = authorBusiness.Get(id);
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author model)
        {
            var response = await authorBusiness.Update(model);
            if (response.IsSucceed)
            {
                return RedirectToAction("Index", "Author");
            }

            ViewBag.Error = response.Message;
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var datas = authorBusiness.Delete(id);
            return RedirectToAction("Index", "Author");
        }

        [HttpGet]
        public IActionResult Active(int id)
        {
            var datas = authorBusiness.Active(id);
            return RedirectToAction("Index", "Author");
        }

        [HttpGet]
        public IActionResult Passive(int id)
        {
            var datas = authorBusiness.Passive(id);
            return RedirectToAction("Index", "Author");
        }
    }
}
