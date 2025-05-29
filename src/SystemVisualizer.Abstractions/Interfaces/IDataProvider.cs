namespace SystemVisualizer.Abstractions.Interfaces;

public interface IDataProvider
{
    (IEnumerable<INode> Nodes, IEnumerable<IConnection> Connections) GetNodes();
    object GetRawData();
}