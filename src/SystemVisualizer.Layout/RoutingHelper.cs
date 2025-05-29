using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Core.Routing;
using SystemVisualizer.Core.Enums;
using SystemVisualizer.Core.Models;

namespace SystemVisualizer.Layout
{
    public static class RoutingHelper
    {
        public static RoutingMode GetRoutingMode(EdgeRoutingMode routingMode)
        {
            if (routingMode == EdgeRoutingMode.StraightLine)
            {
                return RoutingMode.StraightLine;
            }
            if (routingMode == EdgeRoutingMode.Spline || routingMode == EdgeRoutingMode.SplineBundling || routingMode == EdgeRoutingMode.SugiyamaSplines)
            {
                return RoutingMode.Curve;
            }
            return RoutingMode.RightAngle;
        }
    }
}
