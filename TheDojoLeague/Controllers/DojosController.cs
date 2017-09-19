using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheDojoLeague.Models;
using TheDojoLeague.Factory; 

namespace TheDojoLeague.Controllers{
    public class DojosController : Controller{
        private readonly DojoFactory dojoFactory;

        public DojosController(){
            dojoFactory = new DojoFactory();
        }

        [HttpGet]
        [Route("Dojos")]
        public IActionResult Index()
        {
            ViewBag.Dojos = dojoFactory.getDojos();
            return View();
        }

        [HttpPost]
        [Route("addDojo")]
        public IActionResult addDojo(Dojo dojo){
            if(ModelState.IsValid){
                Console.WriteLine("THERE WERE NO ERRORS!");
                dojoFactory.Add(dojo);
                return Redirect("/Dojos");
            }
            else{
                Console.WriteLine("THERE WAS ERRORS IN YOUR FORM!");
                return View("index", dojo);
            }
        }
    }
} 