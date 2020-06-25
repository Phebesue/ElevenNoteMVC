using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ElevenNote.Controllers
{
    public class CategoryController : Controller
    {
        [Authorize]
        // GET: Category/Index
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);
            var model = service.GetCategories();
            //var model = new NoteListItem[0];

            return View(model);
        }
        //GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }
        //POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateCategoryService();

            if (service.CreateCategory(model))
            {
                TempData["SaveResult"] = "Your category was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Category could not be created.");
            return View(model);
        }
        //GET: Category/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateCategoryService();
            var model = svc.GetCategoryById(id);

            return View(model);
        }
        //GET: Category/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateCategoryService();
            var detail = service.GetCategoryById(id);
            var model =
                new CategoryEdit
                {
                    CategoryId = detail.CategoryId,
                    CategoryTitle = detail.CategoryTitle,
                };
            return View(model);
        }
        //POST: Category/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.CategoryId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }
            var service = CreateCategoryService();
            if (service.UpdateCategory(model))
            {
                TempData["SaveResult"] = "Your category was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Category could not be created.");
            return View();
        }
        //Get:Category/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCategoryService();
            var model = svc.GetCategoryById(id);

            return View(model);
        }
        //POST: Category/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateCategoryService();
            svc.DeleteCategory(id);
            TempData["SaveResult"] = "Your category was deleted.";
            return RedirectToAction("Index");
        }

        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);
            return service;
        }
    }
}