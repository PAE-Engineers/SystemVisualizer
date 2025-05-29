using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Prototype.Ranking;

namespace SystemVisualizer.Layout.LayoutCalculators {
    public class RankingLayoutCalculator : LayoutCalculatorBase
    {
        public override LayoutAlgorithmSettings LayoutAlgorithmSettings {
            get {
                return new RankingLayoutSettings() {
                    NodeSeparation = NodeSeparation,
                    EdgeRoutingSettings =
                    {
                        EdgeRoutingMode = EdgeRoutingMode.None,
                        
                    },
                    ClusterMargin = 300,
                    PackingMethod = PackingMethod.Compact,
                    LiftCrossEdges = true,
                    ScaleY = 2,
                    Reporting = true,
                    OmegaY = 2,
                    PackingAspectRatio = 2,
                    
                    

                };
            }
        }
        public override void CalculateLayout(GeometryGraph geometryGraph) {
            var geomGraphComponents = GraphConnectedComponents.CreateComponents(geometryGraph.Nodes, geometryGraph.Edges, NodeSeparation);
            var settings = LayoutAlgorithmSettings as RankingLayoutSettings;
            foreach(var components in geomGraphComponents) {
                var layout = new RankingLayout(settings, components);
                components.Margins = 30;
                layout.Run();
            }
            Microsoft.Msagl.Layout.MDS.MdsGraphLayout.PackGraphs(geomGraphComponents, settings);
            geometryGraph.UpdateBoundingBox();
        }
    }
}
