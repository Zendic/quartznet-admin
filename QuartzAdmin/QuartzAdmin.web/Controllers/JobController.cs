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
        //private Models.JobRepository jobRepo = new QuartzAdmin.web.Models.JobRepository();
        //private Models.GroupRepository groupRepo = new QuartzAdmin.web.Models.GroupRepository();
        Models.InstanceRepository instanceRepo = new QuartzAdmin.web.Models.InstanceRepository();

        //
        // GET: /Job/

        public ActionResult Index(string id)
        {
            Models.InstanceModel instance = instanceRepo.GetInstance(id);

            ViewData["instanceName"] = instance.InstanceName;
            List<Quartz.JobDetail> jobs = instance.GetAllJobs();
            if (jobs == null || jobs.Count == 0)
            {
                return View("NotFound");
            }
            else
            {
                return View(jobs);
            }
            
        }


        public ActionResult Details(string instanceName, string groupName, string itemName)
        {
            Models.InstanceModel instance = instanceRepo.GetInstance(instanceName);
            Models.JobRepository jobRepo = new QuartzAdmin.web.Models.JobRepository(instance);
            Models.TriggerRepository triggerRepo = new QuartzAdmin.web.Models.TriggerRepository(instance);
            
            Quartz.JobDetail job = jobRepo.GetJob(itemName, groupName);
            Models.JobViewModel jvm = new QuartzAdmin.web.Models.JobViewModel();
            jvm.JobDetail = job;
            if (job != null)
            {
                jvm.Triggers = triggerRepo.GetTriggersForJob(itemName, groupName);
            }


            ViewData["instanceName"] = instanceName;
            if (job == null)
            {
                return View("NotFound");
            }
            else
            {
                
                return View(jvm);
            }
        }

        //public ActionResult RunNow
    }
}
