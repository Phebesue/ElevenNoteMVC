using ElevenNote.Models;
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
            var model = new NoteListItem[0];
            return View(model);
        }
    }
}