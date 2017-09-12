using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dojodachi.Controllers
{
    public class Dojodachi
    {
        public int happiness;
        public int fullness;
        public int energy;
        public int meals;

        public Dojodachi()
        {
            happiness = 20;
            fullness = 20;
            energy = 50;
            meals = 3;
        }
    }

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

    public class DojodachiController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("started") == null){
                HttpContext.Session.SetString("started", "GameStarted");
                HttpContext.Session.SetString("action", "Grow you pet and try not to kill it...");
                ViewBag.Action = HttpContext.Session.GetString("action");
                Dojodachi pet = new Dojodachi();
                HttpContext.Session.SetObjectAsJson("pet", pet);
                HttpContext.Session.SetInt32("happiness", pet.happiness);
                HttpContext.Session.SetInt32("fullness", pet.fullness);
                HttpContext.Session.SetInt32("energy", pet.energy);
                HttpContext.Session.SetInt32("meals", pet.meals);
                ViewBag.Fullness = pet.fullness;
                ViewBag.Happiness = pet.happiness;
                ViewBag.Energy = pet.energy;
                ViewBag.Meals = pet.meals;
                ViewBag.Restart = false;
            }
            else{
                ViewBag.Restart = false;
                string message = HttpContext.Session.GetString("action");
                ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
                ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
                ViewBag.Energy = HttpContext.Session.GetInt32("energy");
                ViewBag.Meals = HttpContext.Session.GetInt32("meals");
                if(ViewBag.Energy >= 100 && ViewBag.Fullness >= 100 && ViewBag.Happiness >=100){
                    HttpContext.Session.SetString("action", "Congratulations! You won!");
                    message = HttpContext.Session.GetString("action");
                    ViewBag.Action = message;
                    ViewBag.Restart = true;
                    return View("index");
                }
                if(ViewBag.Fullness <=0 || ViewBag.Happiness <=0){
                    HttpContext.Session.SetString("action", "Your little guy passed :( RIP...");
                    message = HttpContext.Session.GetString("action");
                    ViewBag.Action = message;
                    ViewBag.Restart = true;
                    return View("index");
                }
                message = HttpContext.Session.GetString("action");
                ViewBag.Action = message;
            }
            return View("index");
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult feed()
        {
            Dojodachi current = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            if(current.meals == 0){
                HttpContext.Session.SetString("action", "You ran out of meals smh...");
                return Redirect("/");
            }
            else{
                current.meals -= 1;
                HttpContext.Session.SetInt32("meals", current.meals);
                Random rand = new Random();
                int liked = rand.Next(1,101);
                if(liked <= 25){
                    HttpContext.Session.SetString("action", $"You feed your Dojodachi. Meals -1, It did not like your nasty food");
                }
                else{
                    int randomFullness = rand.Next(5, 11);
                    current.fullness += randomFullness;
                    HttpContext.Session.SetInt32("fullness", current.fullness);
                    HttpContext.Session.SetString("action", $"You feed your Dojodachi. Meals -1, Fullness +{randomFullness}");
                }
                HttpContext.Session.SetObjectAsJson("pet", current);
                return Redirect("/");
            }
        }

        [HttpGet]
        [Route("play")]
        public IActionResult play()
        {
            Dojodachi current = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            current.energy -= 5;
            HttpContext.Session.SetInt32("energy", current.energy);
            Random rand = new Random();
            int liked = rand.Next(1,101);
            if(liked <= 25){
                HttpContext.Session.SetString("action", $"You played with your Dojodachi. It did not like your play time, Energy -5");
            }
            else{
                int randomHappiness = rand.Next(5, 11);
                current.happiness += randomHappiness;
                HttpContext.Session.SetInt32("happiness", current.happiness);
                HttpContext.Session.SetString("action", $"You played with your Dojodachi. Happiness +{randomHappiness}, Energy -5");
            }
            HttpContext.Session.SetObjectAsJson("pet", current);
            return Redirect("/");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult work()
        {
            Dojodachi current = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            current.energy -= 5;
            HttpContext.Session.SetInt32("energy", current.energy);
            Random rand = new Random();
            int randomMeals = rand.Next(1, 4);
            current.meals += randomMeals;
            HttpContext.Session.SetInt32("meals", current.meals);
            HttpContext.Session.SetString("action", $"You worked your Dojodachi. Meals +{randomMeals}, Energy -5");
            HttpContext.Session.SetObjectAsJson("pet", current);
            return Redirect("/");
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult sleep()
        {
            Dojodachi current = HttpContext.Session.GetObjectFromJson<Dojodachi>("pet");
            current.energy += 15;
            HttpContext.Session.SetInt32("energy", current.energy);
            current.fullness -= 5;
            HttpContext.Session.SetInt32("fullness", current.fullness);
            current.happiness -= 5;
            HttpContext.Session.SetInt32("happiness", current.happiness);
            HttpContext.Session.SetString("action", "Your Dojodachi slept. Energy +15, Fullness -5, Happiness -5");
            HttpContext.Session.SetObjectAsJson("pet", current);
            return Redirect("/");
        }

        [HttpGet]
        [Route("restart")]
        public IActionResult restart(){
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
