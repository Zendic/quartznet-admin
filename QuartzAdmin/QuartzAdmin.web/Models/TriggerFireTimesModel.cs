using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzAdmin.web.Models
{
    public class TriggerFireTimesModel
    {
        public Quartz.ICalendar Calendar { get; set; }
        public Quartz.Trigger Trigger { get; set; }
        public InstanceModel Instance { get; set; }
    }
}
