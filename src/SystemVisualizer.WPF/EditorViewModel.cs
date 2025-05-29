using System.Collections.ObjectModel;
using System.ComponentModel;
using Nodify;
using SystemVisualizer.Core;
using SystemVisualizer.Core.Interfaces;
using SystemVisualizer.Core.Models;
using Node = SystemVisualizer.Core.Models.Node;

namespace SystemVisualizer.WPF
{

    public class EditorVm
    {
        public EditorVm()
        {

        }

        
        public GraphObservableCollection<Node> Nodes { get; set; } = new();

        public GraphObservableCollection<Edge> Connections { get; set; } = new();

        public EditorVm(IEnumerable<Node> nodes, IEnumerable<Edge> connections)
        {
            Nodes = new(nodes);
            Connections = new(connections);
        }

    }
}
