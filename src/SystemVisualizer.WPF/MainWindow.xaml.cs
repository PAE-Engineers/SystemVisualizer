using System.Collections.ObjectModel;
using System.Windows;
using SystemVisualizer.Core;
using SystemVisualizer.Core.Models;
using SystemVisualizer.DataProviders;

namespace SystemVisualizer.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _filePath = "C:\\Users\\sam.najjar\\source\\repos\\Revit-System-Visualizer\\src\\SystemVisualizer\\Data\\onelinebackcheck.xlsx";
        private readonly EditorVm _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            var dataConverter = new ExcelDataProvider();
            var data = dataConverter.GetNodes();

            // Convert data.Nodes and data.Connections to ObservableCollection
            var nodes = new ObservableCollection<Node>(data.Nodes);
            var connections = new ObservableCollection<Edge>(data.Connections);

    
            //var vm = GetViewModel();
            var vm = new EditorVm
            {
                Nodes = new(nodes),
                Connections = new(connections)
            };

            _viewModel = vm;

            this.DataContext = _viewModel;
        }

        

        private static EditorVm GetViewModel()
        {
            var vm = new EditorVm
            {
                Nodes = new GraphObservableCollection<Node>(),
                Connections = new GraphObservableCollection<Edge>()
            };

            var welcome = new Node
            {
                Name = "Welcome",
                Inputs = new GraphObservableCollection<Connector>
                {
                    new Connector()
                    {
                        Name = "In",
                        Type = ConnectorType.Input
                    }
                },
                Outputs = new GraphObservableCollection<Connector>
                {
                    new Connector()
                    {
                        Name = "Out",
                        Type = ConnectorType.Output
                    }
                }
            };

            var nodify = new Node
            {
                Name = "To Nodify",
                Inputs = new GraphObservableCollection<Connector>
                {
                    new Connector()
                    {
                        Name = "In",
                        Type = ConnectorType.Input
                    }
                }
            };

            vm.Nodes.Add(welcome);
            vm.Nodes.Add(nodify);

            vm.Connections.Add(new Edge(welcome.Outputs[0], nodify.Inputs[0]));

            return vm;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var layout = new GraphLayout(new DisconnectedGraphsLayoutCalculator());
            var graph = new Graph
            {
                Nodes = _viewModel.Nodes,
                Edges = _viewModel.Connections
            };

            var positions = layout.RelayoutGraphNodesPosition(graph);
            foreach (var position in positions)
            {
                var node = _viewModel.Nodes.FirstOrDefault(n => n.Name == position.Node.Name);
                if (node != null)
                {
                    node.Location = new (position.X, position.Y);
                }
            }
        }
    }

}
