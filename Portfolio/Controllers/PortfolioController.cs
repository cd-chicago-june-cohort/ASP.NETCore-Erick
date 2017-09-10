using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace Portfolio.Controllers
{
    public class PortfolioController : Controller
    {
        [HttpGet]
        [Route("home")]
        public IActionResult home()
        {
            return View("index");
        }

        [HttpGet]
        [Route("projects")]
        public IActionResult projects()
        {
            return View("projects");
        }

        [HttpGet]
        [Route("contact")]
        public IActionResult contact()
        {
            return View("contact");
        }
    }
}
