using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PokeInfo.Controllers
{
    public static class SessionExtensions{
        public static void SetObjectAsJson(this ISession session, string key, object value){
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetObjectFromJson<Dictionary<string, object>>("pokeInfo") == null){
                ViewBag.Pokemon = null;
            }
            else{
                var PokeInfo = new Dictionary<string, object>();
                PokeInfo = HttpContext.Session.GetObjectFromJson<Dictionary<string, object>>("pokeInfo");
                ViewBag.Pokemon = PokeInfo;
            }
            return View("index");
        }

        [HttpGet]
        [Route("pokemon/{pokeid}")]
        public IActionResult QueryPoke(int pokeid)
        {
            var PokeInfo = new Dictionary<string, object>();
            WebRequest.GetPokemonDataAsync(pokeid, ApiResponse =>
                {
                    PokeInfo = ApiResponse;
                    HttpContext.Session.SetObjectAsJson("pokeInfo", PokeInfo);
                    Console.WriteLine(PokeInfo);
                }
            ).Wait();
            return Redirect("/");
        }
    }
}
