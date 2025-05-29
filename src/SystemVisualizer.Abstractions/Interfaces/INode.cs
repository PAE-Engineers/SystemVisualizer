namespace SystemVisualizer.Abstractions.Interfaces;

public interface INode
{
    string Title { get; set; }
    IList<IConnector> Input { get; set; }
    IList<IConnector> Output { get; set; }

    IPoint Location { get; set; }

    int AddInputConnector(IConnector connector);
    int AddOutputConnector(IConnector connector);
    int RemoveInputConnector(IConnector connector);
    int RemoveOutputConnector(IConnector connector);

}