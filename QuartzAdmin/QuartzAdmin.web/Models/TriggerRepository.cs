using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;


namespace QuartzAdmin.web.Models
{
    public class TriggerRepository : BaseQuartzRepository
    {
        public Trigger GetTrigger(string triggerName, string groupName)
        {
            IScheduler sched = GetQuartzScheduler();


            return sched.GetTrigger(triggerName, groupName);

        }

        public IList<TriggerStatusModel> GetAllTriggerStatus(string groupName)
        {
            IScheduler sched = GetQuartzScheduler();
            string[] triggerNames= sched.GetTriggerNames(groupName);
            List<TriggerStatusModel> triggerStatuses = new List<TriggerStatusModel>();
            foreach (string triggerName in triggerNames)
            {
                Trigger trig = sched.GetTrigger(triggerName, groupName);
                TriggerState st = sched.GetTriggerState(triggerName, groupName);
                DateTime? nextFireTime = trig.GetNextFireTimeUtc();
                DateTime? lastFireTime = trig.GetPreviousFireTimeUtc();
                

                triggerStatuses.Add(new TriggerStatusModel()
                {
                    TriggerName = triggerName,
                    GroupName = groupName,
                    State = st,
                    NextFireTime = nextFireTime.HasValue?nextFireTime.Value.ToLocalTime().ToString():"",
                    LastFireTime = lastFireTime.HasValue ? lastFireTime.Value.ToLocalTime().ToString() : "",
                    JobName = trig.JobName
                });

            }

            return triggerStatuses;


        }

    }
}
