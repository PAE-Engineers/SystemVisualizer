using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Geometry;
using SystemVisualizer.Core.Interfaces;
using SystemVisualizer.Core.Models;
using Edge = Microsoft.Msagl.Core.Layout.Edge;

namespace SystemVisualizer.Layout
{
    public static class MsaglHelpers
    {
        public static IEnumerable<(IGraphItem Node, double X, double Y)> GetNodesPositionInfo(GeometryGraph geometryGraph)
        {
            return geometryGraph.Nodes.Select(node =>
                ((IGraphItem)node.UserData, node.BoundingBox.LeftTop.X, node.BoundingBox.LeftTop.Y));
        }

        public static GeometryGraph CreateGeometryGraph(Graph graph) 
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

        private static void AddEdge(GeometryGraph geomGraph, IGraphItem from, IGraphItem to, double weight)
        {
            if (from == null || to == null)
                return;
            geomGraph.Edges.Add(new Edge(AddNode(geomGraph, from), AddNode(geomGraph, to)) { Weight = (int)weight });
        }

        private static Microsoft.Msagl.Core.Layout.Node AddNode(GeometryGraph geometryGraph, IGraphItem node)
        {
            var msaglNode = geometryGraph.FindNodeByUserData(node);
            if (msaglNode == null)
            {
                msaglNode = new Microsoft.Msagl.Core.Layout.Node(CreateCurve(node), node);
                
                geometryGraph.Nodes.Add(msaglNode);
            }

            return msaglNode;
        }

        public static ICurve CreateCurve( IGraphItem item)
        {
            return CurveFactory.CreateRectangle(item.ActualSize.W, item.ActualSize.H, new Point());
        }
    }
}

