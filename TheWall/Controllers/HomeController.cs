using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheWall.Models;
using DbConnection;

namespace TheWall.Controllers
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
                    DbConnector.Execute($"INSERT INTO users(firstName, lastName, email, password, created_at, updated_at) VALUES('{newUser.firstName}', '{newUser.lastName}', '{newUser.email}', '{newUser.password}', NOW(), NOW())");
                    List<Dictionary<string, object>> currentUser = DbConnector.Query("SELECT LAST_INSERT_ID()");
                    HttpContext.Session.SetInt32("currentUser", Convert.ToInt32(currentUser[0]["LAST_INSERT_ID()"]));
                    return RedirectToAction("Wall", "Wall");
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
                HttpContext.Session.SetInt32("currentUser", (int)userLookup[0]["id"]);
                return RedirectToAction("Wall", "Wall");
            }
        }
    }
}
