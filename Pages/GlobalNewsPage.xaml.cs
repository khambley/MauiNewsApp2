using MauiNewsApp2.ViewModels;

namespace MauiNewsApp2.Pages;

public partial class GlobalNewsPage : ContentPage
{
    readonly HeadlinesViewModel viewModel;

    public GlobalNewsPage(HeadlinesViewModel viewModel)
	{
        this.viewModel = viewModel;
        InitializeComponent();
        Task.Run(async () => await Initialize("global"));
    }

    private async Task Initialize(string scope)
    {
        BindingContext = viewModel;
        await viewModel.Initialize(scope);
    }
}
