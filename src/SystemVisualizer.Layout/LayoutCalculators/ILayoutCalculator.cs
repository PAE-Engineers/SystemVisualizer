using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Core.Layout;

namespace SystemVisualizer.Layout.LayoutCalculators
{
    public interface ILayoutCalculator
    {
        LayoutAlgorithmSettings LayoutAlgorithmSettings { get; }

        void CalculateLayout(GeometryGraph graph);
    }
}
