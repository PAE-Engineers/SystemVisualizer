using CommunityToolkit.Mvvm.Messaging.Messages;
using SystemVisualizer.Core.Interfaces;

namespace SystemVisualizer.Messages;

public class SelectedNodeChangedMessage : ValueChangedMessage<IGraphItem>
{
    public IGraphItem Node { get; set; }
    public SelectedNodeChangedMessage(IGraphItem value) : base(value)
    {
        // This message is used to notify that the selected node has changed.
        // It can be used to update the UI or perform other actions based on the selected node.
       Node = value;
    }
}