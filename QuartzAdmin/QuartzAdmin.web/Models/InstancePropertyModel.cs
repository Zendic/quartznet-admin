using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using Iesi.Collections.Generic;

namespace QuartzAdmin.web.Models
{
    [ActiveRecord(Table="tbl_instanceproperties")]
    public class InstancePropertyModel : ActiveRecordBase<InstancePropertyModel>
    {
        [PrimaryKey(Generator=PrimaryKeyType.Identity)]
        public virtual int InstancePropertyID { get; set; }

        [BelongsTo("InstanceID")]
        public virtual InstanceModel ParentInstance{ get; set; }

        [Property(NotNull=true)]
        public virtual string PropertyName { get; set; }

        [Property(NotNull = true)]
        public virtual string PropertyValue { get; set; }
    }
}
