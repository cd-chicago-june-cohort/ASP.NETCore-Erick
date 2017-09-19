using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Restaurant.Models;
using System.Linq;

namespace Restaurant.Controllers
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
        [Route("review")]
        public IActionResult review()
        {
            List<Review> ReturnedValues = _context.reviews.OrderByDescending(review => review.Id).ToList();
            ViewBag.Reviews = ReturnedValues;
            return View("reviews");
        }

        [HttpPost]
        [Route("process")]
        public IActionResult process(Review review){
            if(ModelState.IsValid){
                Review newReivew = new Review
                {
                    Author = review.Author,
                    Restaurant = review.Restaurant,
                    Content = review.Content,
                    Stars = review.Stars,
                    Date = review.Date
                };
                _context.Add(newReivew);
                _context.SaveChanges();
                return Redirect("/review");
            }else{
                return View("index", review);
            }
        }
    }
}
