using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingContext _context;

        public HomeController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Reg()
        {
            return View("Reg");
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult register(RegisterViewModel testUser)
        {
            if (ModelState.IsValid)
            {
                User currentUser = _context.Users.SingleOrDefault(user => user.Email == testUser.Email);
                if(currentUser != null){
                    ModelState.AddModelError("Email", "There is already a registered user with that email.");
                    return View("Reg", testUser);
                }
                User NewUser = new User
                {
                    FirstName = testUser.FirstName,
                    LastName = testUser.LastName,
                    Email = testUser.Email,
                    Password = testUser.Password
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                currentUser = _context.Users.SingleOrDefault(user => user.Email == NewUser.Email);
                HttpContext.Session.SetInt32("currentUser", (int)currentUser.UserId);
                return Redirect("/dashboard");
            }
            else
            {
                return View("Reg", testUser);
            }
        }

        [HttpPost]
        [Route("/loginUser")]
        public IActionResult loginUser(LoginViewModel testLogin)
        {
            if (ModelState.IsValid)
            {
                User currentUser = _context.Users.SingleOrDefault(user => user.Email == testLogin.Email);
                if(currentUser != null){
                    HttpContext.Session.SetInt32("currentUser", (int)currentUser.UserId);
                    return Redirect("/dashboard");
                }
                else{
                    ModelState.AddModelError("Email", "Email or Password is incorrect.");
                    return View("Login", testLogin);
                }
            }
            else
            {
                return View("Login", testLogin);
            }
        }
    }
}
