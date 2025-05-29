using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Miscellaneous;

namespace SystemVisualizer.Layout.LayoutCalculators
{
    public class SugiyamaLayoutCalculator : LayoutCalculatorBase
    {
        public override LayoutAlgorithmSettings LayoutAlgorithmSettings =>
            new SugiyamaLayoutSettings()
            {
                NodeSeparation = NodeSeparation,
                Transformation = PlaneTransformation.Rotation(Math.PI / 2),
                EdgeRoutingSettings = { EdgeRoutingMode = EdgeRoutingMode.StraightLine },
                PackingMethod = PackingMethod.Compact,
                //GridSizeByX = 500,
                GridSizeByY = 1000,
                PackingAspectRatio = 1,
                LiftCrossEdges = true,
                SnapToGridByY = SnapToGridByY.Bottom
            };

        public override void CalculateLayout(GeometryGraph geometryGraph)
        {
            LayoutHelpers.CalculateLayout(geometryGraph, LayoutAlgorithmSettings, null);
        }
    }
}
