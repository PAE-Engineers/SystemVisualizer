using Avalonia.Controls;
using Avalonia.Interactivity;
using SystemVisualizer.ViewModels;

namespace SystemVisualizer.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    
    private async void OpenFileButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            await vm.OpenDataFileAsync();
        }
    }
}