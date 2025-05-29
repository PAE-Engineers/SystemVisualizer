using System;
using System.Collections.Generic;
using System.Text;
using SystemVisualizer.Abstractions.Interfaces;

namespace SystemVisualizer.Abstractions.Models
{
    public class Connection : IConnection
    {
        public IConnector Source { get; set; }
        public IConnector Target { get; set; }
    }

}
