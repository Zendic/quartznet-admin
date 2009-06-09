using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Iesi.Collections.Generic;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;

namespace QuartzAdmin.web.Models
{
    [ActiveRecord(Table="tbl_instances")]
    public class InstanceModel : ActiveRecordValidationBase<InstanceModel>
    {
        public InstanceModel()
        {
            InstanceProperties = new HashedSet<InstancePropertyModel>();
        }

        [PrimaryKey(Generator=PrimaryKeyType.Identity)]
        public virtual int InstanceID { get; set; }
        [Property, ValidateNonEmpty]
        public virtual string InstanceName { get; set; }

        [HasMany(Table = "tbl_instanceproperties",
                 ColumnKey = "InstanceID",
                 Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public virtual ISet<InstancePropertyModel> InstanceProperties { get; set; }


        public IScheduler GetQuartzScheduler()
        {
            System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection();

            foreach (InstancePropertyModel prop in this.InstanceProperties)
            {
                props.Add(prop.PropertyName, prop.PropertyValue);
            }
            ISchedulerFactory sf = new StdSchedulerFactory(props);
            IScheduler sched = sf.GetScheduler();

            return sched;

        }

        public IQueryable<string> FindAllGroups()
        {
            IScheduler sched = this.GetQuartzScheduler();

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
            IScheduler sched = this.GetQuartzScheduler();
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
            IScheduler sched = this.GetQuartzScheduler();
            string[] triggerNames = sched.GetTriggerNames(groupName);

            foreach (string triggerName in triggerNames)
            {
                triggers.Add(sched.GetTrigger(triggerName, groupName));
            }

            return triggers;
        }

        public List<Trigger> GetAllTriggers()
        {
            List<Trigger> triggers = new List<Trigger>();
            var groups = FindAllGroups();
            foreach (string group in groups)
            {
                List<Trigger> groupTriggers = GetAllTriggers(group);
                triggers.AddRange(groupTriggers);
            }

            return triggers;
        }



    }
}
