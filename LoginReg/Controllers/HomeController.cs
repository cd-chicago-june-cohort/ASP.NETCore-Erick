using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginReg.Models;
using DbConnection;

namespace LoginReg.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if((string)TempData["errors"] != ""){
                return View();
            }
            else{
                TempData["errors"] = "";
            }
            return View();
        }

        [HttpPost]
        [Route("processForm")]
        public IActionResult processForm(User newUser)
        {
            if(ModelState.IsValid)
            {
                List<Dictionary<string, object>> userLookup = DbConnector.Query($"SELECT * FROM users WHERE email = '{newUser.email}'");
                if(userLookup.Count == 0){
                    DbConnector.Execute($"INSERT INTO users(firstName, lastName, email, password) VALUES('{newUser.firstName}', '{newUser.lastName}', '{newUser.email}', '{newUser.password}')");
                    return Redirect("/success");
                }else{
                    TempData["errors"] = "User already exist!";
                    return Redirect("/");
                }
            }else{
                return View("index", newUser);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(string email, string password){
            List<Dictionary<string, object>> userLookup = DbConnector.Query($"SELECT * FROM users WHERE email = '{email}'");
            if(userLookup.Count == 0){
                TempData["errors"] = "User does not exist!";
                return Redirect("/");
            }else if((string)userLookup[0]["password"] != password) {
                TempData["errors"] = "Wrong password!";
                return Redirect("/");
            }else{
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
