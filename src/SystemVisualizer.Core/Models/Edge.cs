using SystemVisualizer.Core.Interfaces;

namespace SystemVisualizer.Core.Models
{
    public class Edge : ObservableObject 
    {
        public Edge()
        {

        }

        public Edge(Connector source, Connector target)
        {
            Source = source;
            Target = target;
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }


        private double _weight = 1;

        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        private Connector? _source;

        public Connector? Source
        {
            get => _source;
            set
            {
                if (SetProperty(ref _source, value) && value is not null)
                {
                    Source!.ConnectedTo = this;
                }
            }
        }

        private Connector? _target;

        public Connector? Target
        {
            get => _target;
            set
            {
                if (!SetProperty(ref _target, value)) return;

                if (value is null && Target?.ConnectedTo is not null)
                    Target.ConnectedTo = null;
                else
                    Target!.ConnectedTo = value is not null ? this : null;
            }
        }

    }

}