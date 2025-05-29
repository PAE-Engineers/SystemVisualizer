using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    
    //[ObservableProperty]
    (double X, double Y) _viewportLocation = (0, 0);
    public (double X, double Y) ViewportLocation
    {
        get => _viewportLocation;
        set
        {
                OnPropertyChanging(nameof(ViewportLocation));
                _viewportLocation = value;
             
                OnPropertyChanged(nameof(ViewportLocation));
        }
    }
    
    [ObservableProperty]
    (double Width, double Height) _viewportSize = (1000, 1000);
    
    [RelayCommand]
    public void ApplyLayout()
    {
       _layoutProvider.ApplyLayout(Nodes, Connections, LayoutType.Hierarchical, RoutingMode.RightAngle); 
    }

    [RelayCommand]
    private void ZoomIn()
    {
        ViewportSize = (ViewportSize.Width * 1.1, ViewportSize.Height * 1.1);
    }

    
    [RelayCommand]
    private void ZoomOut()
    {
        ViewportSize = (ViewportSize.Width * .9, ViewportSize.Height * .9);
    }  
       
    [RelayCommand]
    private void ResetViewport()
    {
        ViewportLocation = (0, 0);
        //ViewportSize = (1000, 1000);
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
        
        //ApplyLayout();
    }


}