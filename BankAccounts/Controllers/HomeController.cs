using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAccounts.Models;
using System.Linq;

namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult login()
        {
            return View("login");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult register(RegisterViewModel testUser)
        {
            if (ModelState.IsValid)
            {
                User currentUser = _context.users.SingleOrDefault(user => user.Email == testUser.Email);
                if(currentUser != null){
                    ModelState.AddModelError("Email", "There is already a registered user with that email.");
                    return View("index", testUser);
                }
                User NewUser = new User
                {
                    FirstName = testUser.FirstName,
                    LastName = testUser.LastName,
                    Email = testUser.Email,
                    Password = testUser.Password,
                    Balance = 0.0
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                currentUser = _context.users.SingleOrDefault(user => user.Email == NewUser.Email);
                HttpContext.Session.SetInt32("currentUser", (int)currentUser.Id);
                return Redirect("/account");
            }
            else
            {
                return View("index", testUser);
            }
        }

        [HttpPost]
        [Route("/loginUser")]
        public IActionResult loginUser(LoginViewModel testLogin)
        {
            if (ModelState.IsValid)
            {
                User currentUser = _context.users.SingleOrDefault(user => user.Email == testLogin.Email);
                if(currentUser != null){
                    HttpContext.Session.SetInt32("currentUser", (int)currentUser.Id);
                    return Redirect("/account");
                }
                else{
                    ModelState.AddModelError("Email", "Email or Password is incorrect.");
                    return View("login", testLogin);
                }
            }
            else
            {
                return View("login", testLogin);
            }
        }
    }
}
