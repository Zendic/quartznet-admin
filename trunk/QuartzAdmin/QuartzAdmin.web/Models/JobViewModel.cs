using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzAdmin.web.Models
{
    public class JobViewModel
    {
        public Quartz.JobDetail JobDetail { get; set; }
        public IList<Quartz.Trigger> Triggers { get; set; }
    }
}
