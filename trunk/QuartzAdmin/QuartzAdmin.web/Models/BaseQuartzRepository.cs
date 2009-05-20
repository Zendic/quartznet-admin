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
        protected IScheduler GetQuartzScheduler()
        {
            System.Collections.Specialized.NameValueCollection props = QuartzAdmin.config.Reader.GetQuartzConfig();
            ISchedulerFactory sf = new StdSchedulerFactory(props);
            IScheduler sched = sf.GetScheduler();

            return sched;

        }

    }
}
