namespace SystemVisualizer.Abstractions.Interfaces;

public interface IConnection
{
    IConnector Source { get; set; }
    IConnector Target { get; set; }
}