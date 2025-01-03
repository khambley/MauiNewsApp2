using MauiNewsApp2.ViewModels;

namespace MauiNewsApp2.Pages;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

    void searchBar_SearchButtonPressed(System.Object sender, System.EventArgs e)
    {
		// fixes a bug - keyboard not dismissing when search button pressed
		searchBar.HideSoftInputAsync(System.Threading.CancellationToken.None);
		searchBar.Unfocus();
    }
	
}
