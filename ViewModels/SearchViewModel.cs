using System;
using System.Web;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiNewsApp2.Models;
using MauiNewsApp2.Services;

namespace MauiNewsApp2.ViewModels
{
	public partial class SearchViewModel : ViewModelBase
	{
        [ObservableProperty]
        private string searchItem;

        [ObservableProperty]
        private NewsResult searchResults;

        private readonly INewsService newsService;

        public SearchViewModel(INewsService newsService, INavigate navigation) : base(navigation)
		{
            this.newsService = newsService;
        }

        [RelayCommand]
        public async Task PerformSearch(string query)
        {
            SearchResults = await newsService.GetSearchResults(query);
        }

        [RelayCommand]
        public async Task ItemSelected(object selectedItem)
        {
            var selectedArticle = selectedItem as Article;
            var url = HttpUtility.UrlEncode(selectedArticle?.Url);
            var title = selectedArticle?.Title;
            await Navigation.NavigateTo($"articleview?url={url}&title={title}");
        }
    }
}

