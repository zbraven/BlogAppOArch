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
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly CategoryBusiness categoryBusiness;

        public CategoryController(IWebHostEnvironment environment, CategoryBusiness categoryBusiness)
        {
            _environment = environment;
            this.categoryBusiness = categoryBusiness;
        }
        public IActionResult Index()
        {
            var datas = categoryBusiness.GetAll();
            return View(datas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var datas = new Category();
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            var response = await categoryBusiness.Create(model);
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
            var datas = categoryBusiness.Get(id);
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
            var response = await categoryBusiness.Update(model);
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
            var datas = categoryBusiness.Delete(id);
            return RedirectToAction("Index", "Author");
        }

        [HttpGet]
        public IActionResult Active(int id)
        {
            var datas = categoryBusiness.Active(id);
            return RedirectToAction("Index", "Author");
        }

        [HttpGet]
        public IActionResult Passive(int id)
        {
            var datas = categoryBusiness.Passive(id);
            return RedirectToAction("Index", "Author");
        }
    }
}
