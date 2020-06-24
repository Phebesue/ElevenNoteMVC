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
    public class NoteController : Controller
    {
        [Authorize]
        // GET: Note/Index
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();
            //var model = new NoteListItem[0];

            return View(model);
        }
        //GET: Note/Create
        public ActionResult Create()
        {
            return View();
        }
        //POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (ModelState.IsValid) return View(model);
            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }

        private object CreateNoteService()
        {
            throw new NotImplementedException();
        }

        //private NoteService CreateNoteService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    var service = new NoteService(userId);
        //    return service;
        //}
    }
}
