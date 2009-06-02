using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace QuartzAdmin.web.Controllers
{
    public class JobExecutionController : Controller
    {
        //private Models.JobRepository jobRepo = new QuartzAdmin.web.Models.JobRepository();
        //private Models.TriggerRepository trigRepo = new QuartzAdmin.web.Models.TriggerRepository();
        Models.InstanceRepository instanceRepo = new QuartzAdmin.web.Models.InstanceRepository();


        //
        // GET: /JobExecution/

        public ActionResult Index()
        {
            return View();
        }

        /*
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RunNow(string groupName, string itemName)
        {
            //jobRepo.RunJobNow(itemName, groupName);
            return Content("Job execution started");
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RunNow(string groupName, string itemName, DateTime lastRunDate)
        {
            //jobRepo.RunJobNow(itemName, groupName);
            return Content("Job execution started");
        }
         * */

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RunNow(string instanceName, string groupName, string itemName)
        {
            Models.InstanceModel instance = instanceRepo.GetInstance(instanceName);
            Models.JobRepository jobRepo = new QuartzAdmin.web.Models.JobRepository(instance);

            //var jdm_keys = this.ValueProvider.Keys.Where(k=>k.StartsWith("jdm_"));
            Quartz.JobDetail job = jobRepo.GetJob(itemName, groupName);
            


            foreach (string jdm_key in this.Request.Form.Keys)
            {
                if (jdm_key.StartsWith("jdm_"))
                {
                    if (job.JobDataMap.Contains(jdm_key.Substring(4)))
                    {
                        job.JobDataMap[jdm_key.Substring(4)] = Convert.ChangeType(Request.Form[jdm_key], job.JobDataMap[jdm_key.Substring(4)].GetType());
                    }
                }
            }
            jobRepo.RunJobNow(itemName, groupName, job.JobDataMap);

            return Content("Job execution started");
        }

        public ActionResult CurrentStatus(string id)
        {
            this.ViewData["groupName"] = id;
            return View();
        }

        public JsonResult GetCurrentTriggerStatusList(string id)
        {
            Models.InstanceModel instance = instanceRepo.GetInstance(id);
            Models.TriggerRepository trigRepo = new QuartzAdmin.web.Models.TriggerRepository(instance);
            IList<Models.TriggerStatusModel> triggerStatuses = trigRepo.GetAllTriggerStatus();
            return this.Json(triggerStatuses);
        }

    }
}
