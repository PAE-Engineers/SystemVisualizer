using SystemVisualizer.Core.Enums;
using SystemVisualizer.Core.Models;

namespace SystemVisualizer.Core.Interfaces;

public interface ILayoutProvider
{
    /// <summary>
    /// Applies the specified layout to the graph.
    /// </summary>
    /// <param name="nodes">The nodes of the graph.</param>
    /// <param name="connections">The connections of the graph.</param>
    /// <param name="layoutType">The type of layout to apply.</param>
    /// <param name="routingMode">Edge routing mode</param>
    void ApplyLayout(IEnumerable<IGraphItem> nodes, IEnumerable<Edge> connections, LayoutType layoutType, RoutingMode routingMode);
}