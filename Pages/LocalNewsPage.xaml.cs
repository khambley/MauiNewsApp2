using MauiNewsApp2.ViewModels;

namespace MauiNewsApp2.Pages;

public partial class LocalNewsPage : ContentPage
{
    readonly HeadlinesViewModel viewModel;

    public LocalNewsPage(HeadlinesViewModel viewModel)
	{
        this.viewModel = viewModel;
        InitializeComponent();
        Task.Run(async () => await Initialize("local"));
    }

    private async Task Initialize(string scope)
    {
        BindingContext = viewModel;
        await viewModel.Initialize(scope);
    }
}
