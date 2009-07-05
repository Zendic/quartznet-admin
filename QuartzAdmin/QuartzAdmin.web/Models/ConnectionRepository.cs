using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzAdmin.web.Models
{
    public class ConnectionRepository : IConnectionRepository
    {
        #region IConnectionRepository Members

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public void AddConnection(ConnectionModel connection)
        {
            throw new NotImplementedException();
        }

        public ConnectionModel GetConnection(int connectionId)
        {
            throw new NotImplementedException();
        }

        public void RemoveConnection(ConnectionModel connection)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
