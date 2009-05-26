using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;

namespace QuartzAdmin.web.Models
{
    public class BaseQuartzRepository
    {
        public string InstanceName { get; set; }
        protected IScheduler GetQuartzScheduler()
        {
            //System.Collections.Specialized.NameValueCollection props = QuartzAdmin.config.Reader.GetQuartzConfig();
            System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection();
            /*
            props["quartz.scheduler.instanceName"] = "SampleQuartzScheduler";

            // set thread pool info
            props["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            props["quartz.threadPool.threadCount"] = "1";
            props["quartz.threadPool.threadPriority"] = "Normal";

            // set remoting expoter
            props["quartz.scheduler.proxy"] = "true";
            props["quartz.scheduler.proxy.address"] = "tcp://localhost:555/QuartzScheduler";
            */

            Models.InstanceRepository repo = new InstanceRepository();
            InstanceModel instance = repo.GetInstance(this.InstanceName);
            foreach (InstancePropertyModel prop in instance.InstanceProperties)
            {
                props.Add(prop.PropertyName, prop.PropertyValue);
            }
            ISchedulerFactory sf = new StdSchedulerFactory(props);
            IScheduler sched = sf.GetScheduler();

            return sched;

        }

    }
}
