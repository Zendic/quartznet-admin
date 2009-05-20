using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;

namespace QuartzAdmin.web.Models
{
    public class JobRepository : BaseQuartzRepository
    {

        public JobDetail GetJob(string jobName, string groupName)
        {
            IScheduler sched = GetQuartzScheduler();
            JobDataMap jdm = new JobDataMap();
            
            return sched.GetJobDetail(jobName, groupName);

        }

        public void RunJobNow(string jobName, string groupName)
        {
            IScheduler sched = GetQuartzScheduler();
            sched.TriggerJob(jobName, groupName);
        }
        public void RunJobNow(string jobName, string groupName, JobDataMap jdm)
        {
            
            IScheduler sched = GetQuartzScheduler();
            sched.TriggerJob(jobName, groupName, jdm);
        }

    }
}
