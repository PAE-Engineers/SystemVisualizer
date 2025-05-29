using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using SystemVisualizer.Abstractions.Interfaces;

namespace SystemVisualizer.Abstractions.Models
{
    public class Connector : IConnector
    {
        public string Title { get; set; }
        public IPoint Anchor { get; set; }
        public INode Parent { get; set; }
        public ConnectorType Type { get; set; }

        public bool IsConnected { get; set; }
    }
}
    
