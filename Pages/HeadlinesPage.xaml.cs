using MauiNewsApp2.ViewModels;

namespace MauiNewsApp2.Pages;

public partial class HeadlinesPage : ContentPage
{
    readonly HeadlinesViewModel viewModel;

    public HeadlinesPage(HeadlinesViewModel viewModel)
	{
        this.viewModel = viewModel;
        InitializeComponent();
        Task.Run(async () => await Initialize(GetScopeFromRoute()));
    }
    private async Task Initialize(string scope)
    {
        BindingContext = viewModel;
        await viewModel.Initialize(scope);
    }
    private string GetScopeFromRoute()
    {
        var route = Shell.Current.CurrentState.Location.OriginalString.Split("/").LastOrDefault();
        return route;
    }
}
