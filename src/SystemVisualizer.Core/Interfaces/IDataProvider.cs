using SystemVisualizer.Core.Enums;
using SystemVisualizer.Core.Models;

namespace SystemVisualizer.Core.Interfaces;

public interface IDataProvider
{
    (IEnumerable<IGraphItem> Nodes, IEnumerable<Edge> Connections) GetNodes();
    Task<(IEnumerable<IGraphItem> Nodes, IEnumerable<Edge> Connections)> GetNodes(Stream fileStream, DataFormat format);
    object GetRawData();
}
