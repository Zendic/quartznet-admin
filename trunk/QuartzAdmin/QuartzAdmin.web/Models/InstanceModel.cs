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


    }
}
