using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext _context;

        public WeddingController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            int? currentUserID = HttpContext.Session.GetInt32("currentUser");
            User currentUser =  _context.Users.Include(wedding => wedding.WeddingsAttending).SingleOrDefault(user => user.UserId == currentUserID);
            List<Wedding> ReturnedWeddings = _context.Weddings.Include(w => w.Guests).ThenInclude(g => g.Guest).ToList();

            List<int> weddingIds = new List<int>();
            foreach(var wedding in currentUser.WeddingsAttending){
                weddingIds.Add(wedding.WeddingId);
            }
            ViewBag.Attending = weddingIds;
            ViewBag.Weddings = ReturnedWeddings;
            return View("Dashboard");
        }

        [HttpGet]
        [Route("addWedding")]
        public IActionResult addWedding()
        {
            return View("addWedding");
        }

        [HttpPost]
        [Route("processWedding")]
        public IActionResult processWedding(WeddingViewModel wedding)
        {
            if(ModelState.IsValid){
                Wedding newWedding = new Wedding
                {
                    WedderOne = wedding.WedderOne,
                    WedderTwo = wedding.WedderTwo,
                    Date = wedding.Date,
                    Address = wedding.Address
                };
                _context.Add(newWedding);
                _context.SaveChanges();
                Wedding ReturnedWedding = _context.Weddings.SingleOrDefault(findWedding => findWedding.WedderOne == wedding.WedderOne &&  findWedding.WedderTwo == wedding.WedderTwo);
                int weddingID = ReturnedWedding.WeddingId;
                return RedirectToAction("viewWedding", new { Id = newWedding.WeddingId });
            }
            else{
                return View("addWedding", wedding);
            }
        }

        [HttpGet]
        [Route("viewWedding/{Id}")]
        public IActionResult viewWedding(int Id)
        {
            Wedding ReturnedWedding = _context.Weddings.Include(g => g.Guests).ThenInclude(g => g.Guest).SingleOrDefault(findWedding => findWedding.WeddingId == Id);
            ViewBag.Wedding = ReturnedWedding;
            return View("viewWedding");
        }

        [HttpGet]
        [Route("rsvp/{Id}")]
        public IActionResult RSVP(int Id){
            int? loggedInId = HttpContext.Session.GetInt32("currentUser");
            GuestAtWedding NewConnection = new GuestAtWedding{
                GuestId = (int)loggedInId,
                WeddingId = Id
            };
            _context.GuestAtWedding.Add(NewConnection);
            _context.SaveChanges();
            return Redirect("/dashboard");
        }

        [HttpGet]
        [Route("UnRsvp/{Id}")]
        public IActionResult UnRSVP(int Id){
            int? loggedInId = HttpContext.Session.GetInt32("currentUser");
            GuestAtWedding retrievedConnection = _context.GuestAtWedding.SingleOrDefault(gc => gc.GuestId == loggedInId && gc.WeddingId == Id);
            _context.GuestAtWedding.Remove(retrievedConnection);
            _context.SaveChanges();
            return Redirect("/dashboard");
        }
    }
}
