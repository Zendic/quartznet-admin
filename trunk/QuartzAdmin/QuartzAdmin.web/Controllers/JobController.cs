using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace QuartzAdmin.web.Controllers
{
    public class JobController : Controller
    {
        private Models.JobRepository jobRepo = new QuartzAdmin.web.Models.JobRepository();
        //
        // GET: /Job/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details(string groupName, string itemName)
        {
            Quartz.JobDetail job = jobRepo.GetJob(itemName, groupName);
            
            ViewData["groupName"] = groupName;
            if (job == null)
            {
                return View("NotFound");
            }
            else
            {
                
                return View(job);
            }
        }

        //public ActionResult RunNow
    }
}
