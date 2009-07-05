using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzAdmin.web.Models
{
    public interface IConnectionRepository
    {
        int Count { get; }

        void AddConnection(ConnectionModel connection);
        ConnectionModel GetConnection(int connectionId);
        void RemoveConnection(ConnectionModel connection);
        void Save();

    }
}
