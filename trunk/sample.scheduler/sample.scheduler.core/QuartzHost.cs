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
                job.JobDataMap.Add("ExtraText", "Plinko");
                Quartz.Trigger trigger = Quartz.TriggerUtils.MakeMinutelyTrigger(2);
                trigger.Name = "myFirstTrigger";
                trigger.Group = myGroupName;

                scheduler.ScheduleJob(job, trigger);
            }

            if (!jobNames.Contains("HelloWorld1"))
            {
                Quartz.JobDetail job = new Quartz.JobDetail("HelloWorld1", myGroupName, typeof(Quartz.Job.NoOpJob));
                Quartz.Trigger trigger = Quartz.TriggerUtils.MakeMinutelyTrigger(15);
                trigger.Name = "HelloWorld1Trigger";
                trigger.Group = myGroupName;

                scheduler.ScheduleJob(job, trigger);
            }

            if (!jobNames.Contains("HelloWorld2"))
            {
                Quartz.Impl.Calendar.HolidayCalendar calendar = new Quartz.Impl.Calendar.HolidayCalendar();
                calendar.AddExcludedDate(DateTime.Now.ToUniversalTime());
                calendar.AddExcludedDate(DateTime.Now.AddDays(4).ToUniversalTime());
                scheduler.AddCalendar("randomHolidays", calendar, true, true);

                Quartz.JobDetail job = new Quartz.JobDetail("HelloWorld2", myGroupName, typeof(Quartz.Job.NoOpJob));
                

                Quartz.Trigger trigger = Quartz.TriggerUtils.MakeDailyTrigger(15, 00);
                trigger.Name = "HelloWorld2Trigger";
                trigger.Group = myGroupName;
                trigger.CalendarName = "randomHolidays";
                

                scheduler.ScheduleJob(job, trigger);
            }

            if (!jobNames.Contains("TimeTrackerReminder"))
            {
                Quartz.JobDetail job = new Quartz.JobDetail("TimeTrackerReminder", myGroupName, typeof(Quartz.Job.NoOpJob));
                Quartz.Trigger trigger = Quartz.TriggerUtils.MakeWeeklyTrigger(DayOfWeek.Monday, 8, 0);
                trigger.Name = "EveryMondayAtEight";
                trigger.Group = myGroupName;

                scheduler.ScheduleJob(job, trigger);
            }

            if (!jobNames.Contains("UnscheduledJob"))
            {
                Quartz.JobDetail job = new Quartz.JobDetail("UnscheduledJob", myGroupName, typeof(Quartz.Job.NoOpJob));
                scheduler.AddJob(job, true);
            }



            scheduler.Start();

        }
    }
}
