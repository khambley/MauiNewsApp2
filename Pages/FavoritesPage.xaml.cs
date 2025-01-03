using MauiNewsApp2.ViewModels;

namespace MauiNewsApp2.Pages;

public partial class FavoritesPage : ContentPage
{
	readonly FavoritesViewModel viewModel;
	public FavoritesPage(FavoritesViewModel viewModel)
	{
		this.viewModel = viewModel;
		BindingContext = viewModel;
		InitializeComponent();
	}
}
