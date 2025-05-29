using System.Windows;

namespace SystemVisualizer.Abstractions.Interfaces;

public interface IConnector
{
    bool IsConnected { set; get; }
    string Title { get; set; }
    IPoint Anchor { get; set; }

    INode Parent { get; set; }

    ConnectorType Type { get; set; }
}

public enum ConnectorType
{
    Input,
    Output
}