using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FormSubmission.Models;

namespace FormSubmission.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.noErrors = false;
            return View();
        }

        [HttpPost]
        [Route("processFrom")]
        public IActionResult processFrom(string firstName, string lastName, int AGE, string EMAIL, string PASSWORD){
            User newUser = new User{
                firstName = firstName,
                lastName = lastName,
                age = AGE,
                email = EMAIL,
                password = PASSWORD
            };
            TryValidateModel(newUser);
            ViewBag.errors = ModelState.Values;
            if(ModelState.ErrorCount >= 1){
                ViewBag.noErrors = true;
                return View("index");
            }
            else{
                return Redirect("/success");
            }
        }

        [HttpGet]
        [Route("/success")]
        public IActionResult success()
        {
            return View("success");
        }
    }
}
