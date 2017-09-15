using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace TheWall.Controllers
{
    public class WallController : Controller
    {   
        [HttpGet]
        [Route("Wall")]
        public IActionResult Wall()
        {
            List<Dictionary<string, object>> allMessages = DbConnector.Query("select concat(firstName, ' ', lastName) as name, date_format(messages.created_at, '%b %D %Y') as date, message, messages.created_at, messages.id from messages join users on users_id=users.id order by created_at desc");
            ViewBag.Messages = allMessages;
            List<Dictionary<string, object>> allComments = DbConnector.Query("select concat(firstName, ' ' ,lastName) as name, comments.created_at, comment, messages_id from users join comments on users.id = users_id");
            ViewBag.Comments = allComments;
            return View("index");
        }

        [HttpPost]
        [Route("/addMessage")]
        public IActionResult addMessage(string post){
            int? userID = HttpContext.Session.GetInt32("currentUser");
            DbConnector.Execute($"INSERT INTO messages(users_id, message, created_at, updated_at) VALUES({userID}, '{post}', NOW(), NOW())");
            return Redirect("/Wall");
        }

        [HttpPost]
        [Route("/addComment")]
        public IActionResult addComment(string comment, int messageID){
            int? currentUserID = HttpContext.Session.GetInt32("currentUser");
            DbConnector.Execute($"INSERT INTO comments(messages_id, users_id, comment, created_at, updated_at) value ({messageID}, {currentUserID}, '{comment}', NOW(), NOW())");
            return Redirect("/Wall");
        }
    }
}