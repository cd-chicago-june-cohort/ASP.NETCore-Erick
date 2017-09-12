using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("quotes")]
        public IActionResult viewQuotes()
        {
            List<Dictionary<string, object>> allQuotes = DbConnector.Query("SELECT * FROM quotes ORDER BY created_at DESC");
            ViewBag.Quotes = allQuotes;
            return View("quotes");
        }

        [HttpPost]
        [Route("addQuote")]
        public IActionResult addQuote(string name, string quote)
        {   
            DbConnector.Execute($"INSERT INTO quotes (name, quote, created_at, updated_at) VALUES ('{name}', '{quote}', now(), now())");
            return Redirect("/");
        }
    }
}
