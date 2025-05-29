
using System.Diagnostics;
using SystemVisualizer.Core.Interfaces;

namespace SystemVisualizer.Core.Models
{
    public class Node : ObservableObject, IGraphItem
    {
        public Node()
        {
            Inputs.WhenAdded(c => WhenAdded(c, ConnectorType.Input));
            Outputs.WhenAdded(c => WhenAdded(c, ConnectorType.Input));

            Inputs.WhenRemoved(c => WhenRemoved(c));
        }

        private void WhenRemoved(Connector connector)
        {
            connector.Host = null;
        }

        private void WhenAdded(Connector connector, ConnectorType input)
        {
            connector.Host = this;
            connector.Type = input;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Cluster { get; set; } = string.Empty;

        public GraphObservableCollection<Connector> Inputs { get; set; } = new();
        public GraphObservableCollection<Connector> Outputs { get; set; } = new();

        private (double X, double Y) _location;

        public (double X, double Y) Location
        {
            get => _location;
            set
            {
                SetProperty(ref _location, value);
                Debug.Write($"{Name}: ({Location.X}, {Location.Y}) , {ActualSize.W}X{ActualSize.H}");
            }
        }

        private (double W, double H) _size = (0, 0);
        public (double W, double H) ActualSize
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }


        public override bool Equals(object? obj)
        {
            if (obj is Node node)
            {
                return Name.Equals(node.Name);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
