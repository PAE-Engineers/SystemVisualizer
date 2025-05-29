using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Layout.Incremental;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Miscellaneous;

namespace SystemVisualizer.Layout.LayoutCalculators
{
    public class FastIncrementalLayoutCalculator : LayoutCalculatorBase
    {
        public override LayoutAlgorithmSettings LayoutAlgorithmSettings => new FastIncrementalLayoutSettings()
        {
            NodeSeparation = NodeSeparation,
            AvoidOverlaps = true,
            RouteEdges = false,
            RungeKuttaIntegration = true,
            EdgeRoutingSettings =
            {
                EdgeRoutingMode = EdgeRoutingMode.StraightLine
            },
            PackingMethod = PackingMethod.Compact,
            PackingAspectRatio = 1,
            LiftCrossEdges = true,
        };

        public override void CalculateLayout(GeometryGraph graph)
        {
            LayoutHelpers.CalculateLayout(graph, LayoutAlgorithmSettings, null);
        }
    }
}
