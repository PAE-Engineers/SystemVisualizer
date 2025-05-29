using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Layout.MDS;
using Microsoft.Msagl.Miscellaneous;

namespace SystemVisualizer.Layout.LayoutCalculators {
    public class MDSLayoutCalculator : LayoutCalculatorBase
    {
        public override LayoutAlgorithmSettings LayoutAlgorithmSettings {
            get {
                return new MdsLayoutSettings() {
                    EdgeRoutingSettings = {
                        EdgeRoutingMode = EdgeRoutingMode.StraightLine
                    }
                };
            }
        }
        public override void CalculateLayout(GeometryGraph geometryGraph) {
            LayoutHelpers.CalculateLayout(geometryGraph, LayoutAlgorithmSettings, null);
        }
    }
}
