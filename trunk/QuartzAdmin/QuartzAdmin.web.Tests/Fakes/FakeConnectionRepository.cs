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

        public bool IsValid(ConnectionModel connection, out IEnumerable<RuleViolation> ruleViolations)
        {
            List<RuleViolation> ruleViolationList = new List<RuleViolation>();

            ConnectionModel duplicateName = _connectionList.Where(existingConnection => existingConnection.Name == connection.Name && existingConnection.ConnectionId != connection.ConnectionId).FirstOrDefault();
            if (duplicateName != null)
            {
                ruleViolationList.Add(new RuleViolation("Name is already in use", "Name"));
            }

            ruleViolations = ruleViolationList;
            return (ruleViolationList.Count == 0);
        }

        public void RemoveConnection(ConnectionModel connection)
        {
            _connectionList.Remove(connection);
        }

        public IEnumerable<ConnectionModel> GetConnections()
        {
            return _connectionList;
        }

        public void Save()
        {
        }



    }
}
