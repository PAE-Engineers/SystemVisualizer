using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using SystemVisualizer.Core.Enums;
using SystemVisualizer.Core.Interfaces;
using SystemVisualizer.Core.Models;
using SystemVisualizer.Layout.LayoutCalculators;

namespace SystemVisualizer.Layout
{
    public class GraphLayout
    {
        GeometryGraph GeometryGraph { get; set; }
        EdgeRoutingMode RoutingMode { get { return LayoutCalculator.LayoutAlgorithmSettings.EdgeRoutingSettings.EdgeRoutingMode; } }
        protected ILayoutCalculator LayoutCalculator { get; set; }

        public GraphLayout(ILayoutCalculator layoutCalculator)
        {
            this.LayoutCalculator = layoutCalculator;
        }
        public virtual IEnumerable<(IGraphItem Node, double X, double Y)> RelayoutGraphNodesPosition(Graph graph)
        {
            GeometryGraph = MsaglHelpers.CreateGeometryGraph(graph);
            LayoutCalculator.CalculateLayout(GeometryGraph);
            return MsaglHelpers.GetNodesPositionInfo(GeometryGraph);
        }
        public RoutingMode GetDiagramConnectorType()
        {
            return RoutingHelper.GetRoutingMode(RoutingMode);
        }
    }
}
