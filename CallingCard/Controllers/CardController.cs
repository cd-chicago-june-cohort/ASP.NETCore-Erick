using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class HelloController : Controller
    {

        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Hello World!";
        }

        [HttpGet]
        [Route("{fName}/{lname}/{age}/{favoritInt}/")]
        public IActionResult firstName(string fName, string lName, int age, int favoritInt)
        {
            var user = new {
                         FirstName = fName,
                         LastName = lName,
                         Age = age,
                         FavoriteNumber = favoritInt
                     };
            return Json(user);
        }
    }
}