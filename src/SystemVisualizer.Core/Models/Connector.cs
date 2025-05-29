using SystemVisualizer.Core.Interfaces;

namespace SystemVisualizer.Core.Models
{
    public class Connector : ObservableObject 
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private (double X, double Y) _anchor;

        public (double X, double Y) Anchor
        {
            get => _anchor;
            set => SetProperty(ref _anchor, value);
        }

        private IGraphItem? _host;

        public IGraphItem? Host
        {
            get => _host;
            set => SetProperty(ref _host, value);
        }

        public ConnectorType Type { get; set; }

        private Edge? _connectedTo;

        public Edge? ConnectedTo
        {
            get => _connectedTo;
            set
            {
                if (SetProperty(ref _connectedTo, value))
                {
                    base.OnPropertyChanged(nameof(IsConnected));
                }
            }
        }

        public bool IsConnected => ConnectedTo is not null;
    }
}
    
