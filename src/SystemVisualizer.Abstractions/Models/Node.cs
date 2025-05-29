using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using SystemVisualizer.Abstractions.Interfaces;

namespace SystemVisualizer.Abstractions.Models
{
    class Node : INode

    {
        public string Title { get; set; }
        public IList<IConnector> Input { get; set; } = new List<IConnector>();
        public IList<IConnector> Output { get; set; }= new List<IConnector>();
        public IPoint Location { get; set; }
        public int AddInputConnector(IConnector connector)
        {
            throw new NotImplementedException();
        }

        public int AddOutputConnector(IConnector connector)
        {
            throw new NotImplementedException();
        }

        public int RemoveInputConnector(IConnector connector)
        {
            throw new NotImplementedException();
        }

        public int RemoveOutputConnector(IConnector connector)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Node node)
            {
                return Title.Equals(node.Title);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}
