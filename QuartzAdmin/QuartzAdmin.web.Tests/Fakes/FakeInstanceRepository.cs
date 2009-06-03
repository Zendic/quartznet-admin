using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzAdmin.web.Tests.Fakes
{
    public class FakeInstanceRepository : Models.IInstanceRepository
    {
        #region IInstanceRepository Members

        List<QuartzAdmin.web.Models.InstanceModel> _instances = new List<QuartzAdmin.web.Models.InstanceModel>();

        public void Save(QuartzAdmin.web.Models.InstanceModel instance)
        {
            //throw new NotImplementedException();
            _instances.Add(instance);
        }

        public QuartzAdmin.web.Models.InstanceModel GetByName(string name)
        {
            //throw new NotImplementedException();

            return _instances.Where(x => x.InstanceName == name).FirstOrDefault();
        }


        public List<QuartzAdmin.web.Models.InstanceModel> GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
