using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace MovieAPI.Controllers
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
        [Route("getMovies")]
        public object getMovies()
        {
            List<Dictionary<string, object>> allMovies = DbConnector.Query("SELECT * FROM movies");
            Console.WriteLine("IN BACK END GETTING ALL MOVIES FROM DB");
            Console.WriteLine(allMovies);
            return allMovies;
        }

        [HttpPost]
        [Route("addMovie")]
        public JsonResult addMovie(List<string> movie)
        {
            Console.WriteLine(movie[0]);
            DbConnector.Execute($"INSERT INTO movies(title, releaseDate, rating) VALUES('{movie[0]}', '{movie[1]}', '{movie[2]}')");
            return Json(movie);
        }
    }
}
    