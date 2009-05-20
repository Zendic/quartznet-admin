using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuartzAdmin.web.Controllers
{

    [HandleError]
    public class HomeController : Controller
    {

        Models.GroupRepository groupRepo = new QuartzAdmin.web.Models.GroupRepository();

        public ActionResult Index()
        {
            var groups = groupRepo.FindAllGroups().ToList();
            return View(groups);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
