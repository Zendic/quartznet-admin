using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace QuartzAdmin.web.Controllers
{
    public class GroupController : Controller
    {
        Models.GroupRepository groupRepo = new QuartzAdmin.web.Models.GroupRepository();
        //
        // GET: /Group/

        public ActionResult Index(string id)
        {
            groupRepo.InstanceName = id;
            var groups = groupRepo.FindAllGroups().ToList();

            return View(groups);
        }

        public ActionResult Details(string id)
        {
            Models.GroupViewModel gvm = new QuartzAdmin.web.Models.GroupViewModel();
            gvm.GroupName = id;
            gvm.Jobs = groupRepo.GetAllJobs(id);
            gvm.Triggers = groupRepo.GetAllTriggers(id);
            
            if (gvm.Jobs == null && gvm.Triggers ==null)
            {
                ViewData["groupName"] = id;
                return View("NotFound");
            }
            else
            {
                return View(gvm);
            }

        }

    }
}
