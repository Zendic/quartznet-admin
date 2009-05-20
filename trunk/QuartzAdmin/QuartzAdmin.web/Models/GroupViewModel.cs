using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzAdmin.web.Models
{
    public class GroupViewModel
    {
        public string GroupName { get; set; }
        public List<Quartz.JobDetail> Jobs { get; set; }
        public List<Quartz.Trigger> Triggers { get; set; }

    }
}
