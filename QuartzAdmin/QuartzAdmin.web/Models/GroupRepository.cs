using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;

namespace QuartzAdmin.web.Models
{
    public class GroupRepository : BaseQuartzRepository
    {
        private InstanceModel quartzInstance;
        public GroupRepository(string instanceName)
        {
            InstanceRepository repo = new InstanceRepository();
            quartzInstance = repo.GetInstance(instanceName);
        }

        public GroupRepository(InstanceModel instance)
        {
            quartzInstance = instance;
        }

        public IQueryable<string> FindAllGroups()
        {
            IScheduler sched = quartzInstance.GetQuartzScheduler();

            List<string> groups = new List<string>();

            string[] jobGroups = sched.JobGroupNames;
            string[] triggerGroups = sched.TriggerGroupNames;

            foreach (string jg in jobGroups)
            {
                groups.Add(jg);
            }

            foreach (string tg in triggerGroups)
            {
                if (!groups.Contains(tg))
                {
                    groups.Add(tg);
                }
            }

            return sched.JobGroupNames.AsQueryable();
        }

        public List<JobDetail> GetAllJobs(string groupName)
        {
            List<JobDetail> jobs = new List<JobDetail>();
            IScheduler sched = quartzInstance.GetQuartzScheduler();
            string[] jobNames = sched.GetJobNames(groupName);
            
            foreach (string jobName in jobNames)
            {
                jobs.Add(sched.GetJobDetail(jobName, groupName));
            }

            return jobs;
        }

        public List<JobDetail> GetAllJobs()
        {
            List<JobDetail> jobs = new List<JobDetail>();
            var groups = FindAllGroups();
            foreach (string group in groups)
            {
                List<JobDetail> groupJobs = GetAllJobs(group);
                jobs.AddRange(groupJobs);
            }
            return jobs;
        }

        public List<Trigger> GetAllTriggers(string groupName)
        {
            List<Trigger> triggers = new List<Trigger>();
            IScheduler sched = quartzInstance.GetQuartzScheduler();
            string[] triggerNames = sched.GetTriggerNames(groupName);

            foreach (string triggerName in triggerNames)
            {
                triggers.Add(sched.GetTrigger(triggerName, groupName));
            }

            return triggers;
        }

    }
}
