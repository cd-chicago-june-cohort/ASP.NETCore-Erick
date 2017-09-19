using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheDojoLeague.Models;
using TheDojoLeague.Factory; 

namespace TheDojoLeague.Controllers{
    public class NinjasController : Controller{
        private readonly NinjaFactory ninjaFactory;

        public NinjasController(){
            ninjaFactory = new NinjaFactory();
        }

        [HttpGet]
        [Route("Ninjas")]
        public IActionResult Index()
        {
            ViewBag.Dojos = ninjaFactory.findAll();
            return View();
        }

        [HttpPost]
        [Route("addNinja")]
        public IActionResult addNinja(Ninja ninja, int dojoID){
            if(ModelState.IsValid){
                Console.WriteLine("THERE WERE NO ERRORS!");
                ninja.Dojo_Id = dojoID;
                // ninjaFactory.Add(ninja);
                return Redirect("/");
            }
            else{
                Console.WriteLine("THERE WAS ERRORS IN YOUR FORM!");
                return View("index", ninja);
            }
        }
    }
} 