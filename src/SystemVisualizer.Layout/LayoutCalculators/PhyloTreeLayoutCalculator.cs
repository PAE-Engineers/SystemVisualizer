using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Layout.Layered;

namespace SystemVisualizer.Layout.LayoutCalculators {
    public class PhyloTreeLayoutCalculator : LayoutCalculatorBase
    {
        public override LayoutAlgorithmSettings LayoutAlgorithmSettings {
            get {
                return new SugiyamaLayoutSettings() {
                    Transformation = PlaneTransformation.Rotation(Math.PI),
                    EdgeRoutingSettings = {
                        EdgeRoutingMode =  EdgeRoutingMode.StraightLine,
                    }
                };
            }
        }
        public override void CalculateLayout(GeometryGraph phyloTree) {
            Microsoft.Msagl.Miscellaneous.LayoutHelpers.CalculateLayout(phyloTree, LayoutAlgorithmSettings, null);
        }
    }
}
