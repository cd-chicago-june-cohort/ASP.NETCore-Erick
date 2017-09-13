using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace AjaxNotes.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Dictionary<string, object>> allNotes = DbConnector.Query("SELECT * FROM notes ORDER BY created_at DESC");
            ViewBag.Notes = allNotes;
            return View();
        }

        [HttpPost]
        [Route("addNote")]
        public IActionResult addNote(string noteName)
        {
            string content = "";
            DbConnector.Execute($"INSERT INTO notes(title, content, created_at, updated_at) VALUES('{noteName}', '{content}', now(), now())");
            return Redirect("/");
        }

        [HttpPost]
        [Route("deleteNote")]
        public IActionResult deleteNote(int noteID){
            DbConnector.Execute($"DELETE FROM notes WHERE id={noteID}");
            return Redirect("/");
        }

        [HttpPost]
        [Route("updateNote")]
        public IActionResult updateNote(string content, int noteID){
            DbConnector.Execute($"UPDATE notes SET content='{content}' WHERE id IN ({noteID})");
            return Redirect("/");
        }
    }
}
