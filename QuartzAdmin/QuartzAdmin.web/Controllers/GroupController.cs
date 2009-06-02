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
        Models.InstanceRepository instanceRepo = new QuartzAdmin.web.Models.InstanceRepository();

        //
        // GET: /Group/

        public ActionResult Index(string id)
        {
            Models.InstanceModel instance = instanceRepo.GetInstance(id);
            Models.GroupRepository groupRepo = new QuartzAdmin.web.Models.GroupRepository(instance);


            var groups = groupRepo.FindAllGroups().ToList();

            return View(groups);
        }

        public ActionResult Details(string id)
        {
            Models.InstanceModel instance = instanceRepo.GetInstance(id);
            Models.GroupRepository groupRepo = new QuartzAdmin.web.Models.GroupRepository(instance);
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
