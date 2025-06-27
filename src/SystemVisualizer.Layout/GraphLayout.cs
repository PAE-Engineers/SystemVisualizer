using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using SystemVisualizer.Core.Enums;
using SystemVisualizer.Core.Interfaces;
using SystemVisualizer.Core.Models;
using SystemVisualizer.Layout.LayoutCalculators;
using Edge = Microsoft.Msagl.Core.Layout.Edge;

namespace SystemVisualizer.Layout
{
    public class GraphLayout
    {
        GeometryGraph GeometryGraph { get; set; }
        EdgeRoutingMode EdgeRoutingMode { get { return LayoutCalculator.LayoutAlgorithmSettings.EdgeRoutingSettings.EdgeRoutingMode; } }
        protected ILayoutCalculator LayoutCalculator { get; set; }

        public GraphLayout(ILayoutCalculator layoutCalculator)
        {
            this.LayoutCalculator = layoutCalculator ?? throw new ArgumentNullException(nameof(layoutCalculator));
        }

        public virtual IEnumerable<(IGraphItem Node, double X, double Y)> RelayoutGraphNodesPosition(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            GeometryGraph = CreateGeometryGraph(graph);
            LayoutCalculator.CalculateLayout(GeometryGraph);
            return GetNodesPositionInfo(GeometryGraph);
        }

        public RoutingMode GetDiagramConnectorType()
        {
            if (EdgeRoutingMode == EdgeRoutingMode.StraightLine)
            {
                return RoutingMode.StraightLine;
            }
            if (EdgeRoutingMode == EdgeRoutingMode.Spline || 
                EdgeRoutingMode == EdgeRoutingMode.SplineBundling || 
                EdgeRoutingMode == EdgeRoutingMode.SugiyamaSplines)
            {
                return RoutingMode.Curve;
            }
            return RoutingMode.RightAngle;
        }

        #region Graph Transformation Methods

        private IEnumerable<(IGraphItem Node, double X, double Y)> GetNodesPositionInfo(GeometryGraph geometryGraph)
        {
            return geometryGraph.Nodes.Select(node =>
                ((IGraphItem)node.UserData, node.BoundingBox.LeftTop.X, node.BoundingBox.LeftTop.Y));
        }

        private GeometryGraph CreateGeometryGraph(Graph graph) 
        {
            GeometryGraph geomGraph = new GeometryGraph();

            foreach (var node in graph.Nodes)
            {
                AddNode(geomGraph, node);
            }

            foreach (var edge in graph.Edges.Reverse())
            {
                if(edge.Source is null || edge.Target is null)
                    continue;

                AddEdge(geomGraph, edge.Source.Host, edge.Target.Host, edge.Weight);
            }

            return geomGraph;
        }

        private void AddEdge(GeometryGraph geomGraph, IGraphItem from, IGraphItem to, double weight)
        {
            if (from == null || to == null)
                return;

            geomGraph.Edges.Add(new Edge(AddNode(geomGraph, from), AddNode(geomGraph, to)) { Weight = (int)weight });
        }

        private Microsoft.Msagl.Core.Layout.Node AddNode(GeometryGraph geometryGraph, IGraphItem node)
        {
            var msaglNode = geometryGraph.FindNodeByUserData(node);
            if (msaglNode == null)
            {
                msaglNode = new Microsoft.Msagl.Core.Layout.Node(CreateCurve(node), node);
                geometryGraph.Nodes.Add(msaglNode);
            }

            return msaglNode;
        }

        private ICurve CreateCurve(IGraphItem item)
        {
            return CurveFactory.CreateRectangle(item.Bounds.Width + 500, item.Bounds.Height, new Point());
        }

        #endregion
    }
}
