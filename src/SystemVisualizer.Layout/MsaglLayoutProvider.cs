using SystemVisualizer.Core.Enums;
using SystemVisualizer.Core.Interfaces;
using SystemVisualizer.Core.Models;
using SystemVisualizer.Layout.LayoutCalculators;

namespace SystemVisualizer.Layout;

public class MsaglLayoutProvider : ILayoutProvider
{
    public void ApplyLayout(IEnumerable<IGraphItem> nodes, IEnumerable<Edge> connections, LayoutType layoutType, RoutingMode routingMode)
    {
        var layout = new GraphLayout(new DisconnectedGraphsLayoutCalculator());
        var graph = new Graph
        {
            Nodes = nodes,
            Edges = connections 
        };

        var positions = layout.RelayoutGraphNodesPosition(graph);
        foreach (var position in positions)
        {
            var node = nodes.FirstOrDefault(n => n.Name == position.Node.Name);
            if (node != null)
            {
                node.Location = new (position.X, position.Y);
            }
        } 
    }

}