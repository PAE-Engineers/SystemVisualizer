using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SystemVisualizer.Core.Enums;
using SystemVisualizer.Core.Interfaces;
using SystemVisualizer.Core.Models;

namespace SystemVisualizer.ViewModels;

public partial class EditorViewModel : ViewModelBase
{
    
    private readonly ILayoutProvider _layoutProvider;
    
    public EditorViewModel( ILayoutProvider layoutProvider)
    {
        _layoutProvider = layoutProvider;
    }
    
    //private SourceCache<IGraphItem, string> _nodesCache = new SourceCache<IGraphItem, string>(x => x.Name);
    
    [ObservableProperty]
    private ObservableCollection<IGraphItem> _nodes = new();

    [ObservableProperty]
    private ObservableCollection<IGraphItem> _selectedNodes = new();
    
    [ObservableProperty]
    private IGraphItem _selectedNode;

    [ObservableProperty] 
    private ObservableCollection<Edge> _connections = new();

    [ObservableProperty] 
    private ObservableCollection<Edge> _selectedConnections = new();
    
    [ObservableProperty]
    private string _searchText = string.Empty;
    
    [ObservableProperty]
    Point _viewportLocation = new();
    
    [ObservableProperty]
    double _viewportZoom;
    
    [RelayCommand]
    public void ApplyLayout()
    {
       _layoutProvider.ApplyLayout(Nodes, Connections, LayoutType.Hierarchical, RoutingMode.RightAngle); 
    }

    [RelayCommand]
    private void ZoomIn()
    {
        ViewportZoom *= 1.1; //(ViewportSize.Width * 1.1, ViewportSize.Height * 1.1);
    }
    
    [RelayCommand]
    private void ZoomOut()
    {
        ViewportZoom *= .9; // (ViewportSize.Width * .9, ViewportSize.Height * .9);
    }  
       
    [RelayCommand]
    private void ResetViewport()
    {
        ViewportLocation = new(0, 0);
    } 
    
    public void LoadData(IEnumerable<IGraphItem> nodes, IEnumerable<Edge> connections)
    {
        Nodes.Clear();
        Connections.Clear();
        foreach (var node in nodes)
        {
            Nodes.Add(node);
        }
        foreach (var connection in connections)
        {
            Connections.Add(connection);
        }
        
        ApplyLayout();
    }


}