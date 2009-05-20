using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace sample.scheduler.core
{
    public class QuartzHost
    {
        public void StartScheduler()
        {
            Quartz.Impl.StdSchedulerFactory factory = new Quartz.Impl.StdSchedulerFactory();
            Quartz.IScheduler scheduler = factory.GetScheduler();

            string myJobName = "MyFirstJob";
            string myGroupName="MyGroup";
            string[] jobNames = scheduler.GetJobNames(myGroupName);

            if (!jobNames.Contains(myJobName))
            {
                Quartz.JobDetail job = new Quartz.JobDetail(myJobName, myGroupName, typeof(ConsoleJob1));
                Quartz.Trigger trigger = Quartz.TriggerUtils.MakeMinutelyTrigger(2);
                trigger.Name = "myFirstTrigger";
                trigger.Group = myGroupName;

                scheduler.ScheduleJob(job, trigger);
            }


            scheduler.Start();

        }
    }
}
