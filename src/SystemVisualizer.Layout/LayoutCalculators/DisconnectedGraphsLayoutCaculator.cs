using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Layout.Incremental;
using Microsoft.Msagl.Layout.Layered;

namespace SystemVisualizer.Layout.LayoutCalculators {
    public class DisconnectedGraphsLayoutCalculator : LayoutCalculatorBase {
        public override LayoutAlgorithmSettings LayoutAlgorithmSettings {
            get {
                return new SugiyamaLayoutSettings() {
                    Transformation = PlaneTransformation.Rotation(Math.PI/2),
                    EdgeRoutingSettings = {
                        EdgeRoutingMode =  EdgeRoutingMode.None,
                    },
                    NodeSeparation = NodeSeparation,
                    //GridSizeByX = 60,
                    //GridSizeByY = 200,
                    PackingMethod = PackingMethod.Columns,
                    LiftCrossEdges = true,
                    SnapToGridByY = SnapToGridByY.Bottom,
                    MinNodeHeight = 100,
                    AspectRatio = 3
                };
            }
        }

        public override void CalculateLayout(GeometryGraph geometryGraph) {
            var geomGraphComponents = GraphConnectedComponents.CreateComponents(geometryGraph.Nodes, geometryGraph.Edges, NodeSeparation);
            var settings = LayoutAlgorithmSettings  as SugiyamaLayoutSettings;
            foreach(var components in geomGraphComponents) {
                var layout = new LayeredLayout(components, settings);
                components.Margins = 1000;
                layout.Run();
            }
            Microsoft.Msagl.Layout.MDS.MdsGraphLayout.PackGraphs(geomGraphComponents, settings);

            geometryGraph.UpdateBoundingBox();
        }
    }
}
