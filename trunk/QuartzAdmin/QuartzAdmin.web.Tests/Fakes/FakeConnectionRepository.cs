using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuartzAdmin.web.Models;

namespace QuartzAdmin.web.Tests.Fakes
{
    public class FakeConnectionRepository : IConnectionRepository
    {
        private List<ConnectionModel> _connectionList = new List<ConnectionModel>();

        public int Count
        {
            get { return _connectionList.Count; }
        }

        public void AddConnection(ConnectionModel connection)
        {
            _connectionList.Add(connection);
        }

        public ConnectionModel GetConnection(int connectionId)
        {
            return _connectionList.Where(connection => connection.ConnectionId == connectionId).FirstOrDefault();
        }

        public void RemoveConnection(ConnectionModel connection)
        {
            _connectionList.Remove(connection);
        }

        public void Save()
        {
            var nameGroups =
                from connection in this._connectionList
                group connection by connection.Name into nameGroup
                select new { Name = nameGroup.Key, Connections = nameGroup };

            var duplicateNameGroups =
                from nameGroup in nameGroups
                where nameGroup.Connections.Count() > 1
                select nameGroup;

            int duplicateCount = duplicateNameGroups.Count();

            if (duplicateCount > 0)
            {
                throw new ApplicationException("Duplicate connection name");
            }
        }
    }
}
