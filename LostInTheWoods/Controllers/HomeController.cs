using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LostInTheWoods.Factory; 
using LostInTheWoods.Models;

namespace LostInTheWoods.Controllers
{
    public class HomeController : Controller
    {

        private readonly TrailFactory trailFactory;

        public HomeController(){
            trailFactory = new TrailFactory();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Trails = trailFactory.getTrails();
            return View();
        }

        [HttpGet]
        [Route("/addTrail")]
        public IActionResult addTrail()
        {
            return View("addTrail");
        }

        [HttpGet]
        [Route("/trails/{Id}")]
        public IActionResult addTrail(int Id)
        {   
            ViewBag.UniqueTrail = trailFactory.FindByID(Id);
            return View("trail");
        }

        [HttpPost]
        [Route("/process")]
        public IActionResult process(Trail trail)
        {
            if(ModelState.IsValid){
                Console.WriteLine("THERE WERE NO ERRORS!");
                trailFactory.Add(trail);
                return Redirect("/");
            }
            else{
                Console.WriteLine("THERE WAS ERRORS IN YOUR FORM!");
                return View("addTrail", trail);
            }
        }
    }
}
