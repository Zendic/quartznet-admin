using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;

namespace QuartzAdmin.web.Models
{
    public class CalendarRepository : BaseQuartzRepository
    {
        public ICalendar GetCalendar(string calendarName)
        {
            IScheduler sched = GetQuartzScheduler();
            return sched.GetCalendar(calendarName);
            
        }
    }
}
