
using SystemVisualizer.Core.Interfaces;

namespace SystemVisualizer.Core.Models
{
    public class Graph : ObservableObject 
    {
        private IEnumerable<IGraphItem> _nodes = new List<IGraphItem>();

        public IEnumerable<IGraphItem> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }

        private IEnumerable<Edge> _connections = new List<Edge>();

        public IEnumerable<Edge> Edges
        {
            get => _connections;
            set => SetProperty(ref _connections, value);
        }
    }
 }
