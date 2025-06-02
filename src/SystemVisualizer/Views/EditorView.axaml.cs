using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using SystemVisualizer.Messages;

namespace SystemVisualizer.Views;

public partial class EditorView : UserControl
{
    public EditorView()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<SelectedNodeChangedMessage>(this, (r, m) =>
        {
            if (m.Value is not null)
            {
                // This will scroll the view to the selected node.
                this.NodeEditor.BringIntoView(m.Node.Location);
            }
        });
    }
}