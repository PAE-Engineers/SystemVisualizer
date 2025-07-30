using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SystemVisualizer.Core.Interfaces;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using SystemVisualizer.Core.Enums;
using SystemVisualizer.Layout;

namespace SystemVisualizer.ViewModels;

public partial class MainViewModel(IDataProvider dataProvider) : ViewModelBase
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Nodes))]
    private EditorViewModel _editor= new EditorViewModel( new MsaglLayoutProvider());
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Nodes))]
    private string _searchText = string.Empty;

    public ObservableCollection<IGraphItem> Nodes =>
        new ObservableCollection<IGraphItem>(Editor.Nodes
            .Where(n => SearchText == string.Empty || n.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            .OrderBy(n => n.Name)
        );

    [ObservableProperty]
    private IGraphItem _selectedNode;

    partial void OnSelectedNodeChanged(IGraphItem? value)
    {
        Editor.SelectedNode = value;
        // if (value is not null)
        // {
        //     Editor.SelectedNodes.Clear();
        //     Editor.SelectedNodes.Add(value);
        // }
        // else
        // {
        //     Editor.SelectedNodes.Clear();
        // }
    }
    
    [RelayCommand]
    private void SelectionChanged(IGraphItem? node)
    {
        if (node is not null)
        {
            SelectedNode = node;
            Editor.SelectedNodes.Clear();
            Editor.SelectedNodes.Add(node);
        }
        else
        {
            SelectedNode = null;
            Editor.SelectedNodes.Clear();
        }
    }
    
    public async Task OpenDataFileAsync()
    {
        // Get the storage provider from the current TopLevel
        var options = new FilePickerOpenOptions
        {
            Title = "Select Data File",
            AllowMultiple = false,
            FileTypeFilter = new[] 
            { 
                new FilePickerFileType("Data Files")
                {
                    Patterns = new[] { "*.xlsx", "*.xls", "*.csv" },
                    MimeTypes = new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/vnd.ms-excel", "text/csv" }
                }
            }
        };
    
        var files = await App.StorageProvider.OpenFilePickerAsync(options);
        var file = files.FirstOrDefault();
        
        if (file != null)
        {
            var extension = Path.GetExtension(file.Name);
            var format = extension == ".xlsx" || extension == ".xls" ? DataFormat.Excel : DataFormat.CSV;
            await using var stream = await file.OpenReadAsync();
            await ProcessDataFile(stream, format);
        }
    }
    
    private async Task ProcessDataFile(Stream stream, DataFormat format)
    {
        var (nodes, connections) = await dataProvider.GetNodes(stream, format);
        Editor.LoadData(nodes, connections);
        
        OnPropertyChanged(nameof(Nodes));
    }
    
}
