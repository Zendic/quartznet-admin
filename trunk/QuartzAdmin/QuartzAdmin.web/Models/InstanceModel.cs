using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using Iesi.Collections.Generic;

namespace QuartzAdmin.web.Models
{
    [ActiveRecord(Table="tbl_instances")]
    public class InstanceModel : Castle.ActiveRecord.ActiveRecordBase<InstanceModel>
    {
        public InstanceModel()
        {
            InstanceProperties = new HashedSet<InstancePropertyModel>();
        }
        [PrimaryKey(Generator=PrimaryKeyType.Identity)]
        public virtual int InstanceID { get; set; }
        [Property]
        public virtual string InstanceName { get; set; }

        [HasMany(Table = "tbl_instanceproperties",
                 ColumnKey = "InstanceID",
                 Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public virtual ISet<InstancePropertyModel> InstanceProperties { get; set; }

    }
}
