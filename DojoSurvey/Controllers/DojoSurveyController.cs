using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace DojoSurvey.Controllers
{
    public class DojoSurveyController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("index");
        }

        [HttpPost]
        [Route("/process")]
        public IActionResult process(string name, string location, string language, string comment){
            ViewBag.Name = name;
            ViewBag.Location = location;
            ViewBag.Language = language;
            ViewBag.Comment = comment;
            return View("result");
            // return Redirect("/result");
        }

        // [HttpGet]
        // [Route("/result")]
        // public IActionResult result(){
        //     return View("result");
        // }
    }
}
