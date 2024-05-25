using BetterWutheringWaves.ViewModel.Windows;
using System.Windows;

namespace BetterWutheringWaves.View.Windows;

public partial class MapViewer
{
    public MapViewerViewModel ViewModel { get; }

    public MapViewer()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }
}
