using Microsoft.Msagl.Core.Layout;

namespace SystemVisualizer.Layout.LayoutCalculators;

public abstract class LayoutCalculatorBase : ILayoutCalculator
{
    public static double NodeSeparation = 60;

    public abstract LayoutAlgorithmSettings LayoutAlgorithmSettings { get; }
    public abstract void CalculateLayout(GeometryGraph graph);
}