using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {
    public class GroupController : Controller {
        List<Group> allGroups {get; set;}
        public GroupController() {
            allGroups = JsonToFile<Group>.ReadJson();
        }

        [HttpGet]
        [Route("groups")]
        public JsonResult allGroupsFromApi(){
            return Json(allGroups); 
        }

        [HttpGet]
        [Route("groups/name/{groupName}")]
        public JsonResult groupNames(string groupName){
            List<Group> groupNamesResult = allGroups.Where(group => group.GroupName == groupName).ToList();
            return Json(groupNamesResult); 
        }

        [HttpGet]
        [Route("groups/id/{id}")]
        public JsonResult groupIds(int id){
            List<Group> groupIdsResult = allGroups.Where(group => group.Id == id).ToList();
            return Json(groupIdsResult); 
        }
    }
}