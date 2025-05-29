
using System.Diagnostics;
using Avalonia;
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
            Debug.Write($"Created Node: {Name}");
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

        private Point _location = new();
        public Point Location
        {
            get => _location;
            set
            {
                SetProperty(ref _location, value);
                Debug.WriteLine($"{Name}: ({Location.X}, {Location.Y}) , {Width}X{Height}");
            }
        }

        private Rect _bounds = new Rect(0, 0, 100, 100);

        public Rect Bounds
        {
            get => _bounds;
            set
            {
                SetProperty(ref _bounds, value);
                Debug.WriteLine($"{Name}: Bounds updated to {value.Width}x{value.Height} at ({value.X}, {value.Y})");
            }
        }

        private double _height;
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private double _width;
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
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
