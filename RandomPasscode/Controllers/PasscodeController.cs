using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RandomPasscode.Controllers
{
    public class PasscodeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("passcode") == null){
                HttpContext.Session.SetString("passcode", "");
                HttpContext.Session.SetInt32("counter", 0);
            }
            else{
                ViewBag.Passcode = HttpContext.Session.GetString("passcode");
                ViewBag.Counter = HttpContext.Session.GetInt32("counter");
            }
            return View("index");
        }

        [HttpGet]
        [Route("generate")]
        public IActionResult generate(){
            Random rnd = new Random();
            string newPasscode = "";
            string[] strArray = new string[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            for(var k=0;k<14;k++){
                if(k%2 == 0){
                    newPasscode += strArray[rnd.Next(0, strArray.Length - 1)];
                }
                else{
                    newPasscode += rnd.Next(0, 10).ToString();
                }
            }
            HttpContext.Session.SetString("passcode", newPasscode);
            updateCounter();
            return Redirect("/");
        }

        public void updateCounter(){
            int? counter = HttpContext.Session.GetInt32("counter");
            counter += 1;
            HttpContext.Session.SetInt32("counter", (int)counter);
        }
    }
}
