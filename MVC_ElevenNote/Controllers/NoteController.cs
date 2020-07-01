﻿using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
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
            if (!ModelState.IsValid) return View(model);
            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }
        //GET: Note/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }
        //GET: Note/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }
        //POST: Note/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }
            var service = CreateNoteService();
            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Note could not be created.");
            return View();
        }
        //Get:Note/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }
        //POST: Note/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateNoteService();
            svc.DeleteNote(id);
            TempData["SaveResult"] = "Your note was deleted.";
            return RedirectToAction("Index");
        }

        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}
