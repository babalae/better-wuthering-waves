using BetterWutheringWaves.ViewModel.Pages;

namespace BetterWutheringWaves.View.Pages;

public partial class ScriptControlPage
{
    public ScriptControlViewModel ViewModel { get; }

    public ScriptControlPage(ScriptControlViewModel viewModel)
    {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}
