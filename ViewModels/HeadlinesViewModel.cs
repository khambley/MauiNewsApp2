using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Web;
using MauiNewsApp2.Services;
using MauiNewsApp2.Models;

namespace MauiNewsApp2.ViewModels
{
	public partial class HeadlinesViewModel : ViewModelBase
	{
        private readonly INewsService newsService;

        [ObservableProperty]
        private NewsResult currentNews;

        [ObservableProperty]
        private bool isFavorite;

        public string FavoriteIcon => IsFavorite ? "heart_filled.svg" : "heart_outline.svg";

        public HeadlinesViewModel(INewsService newsService, INavigate navigation) : base (navigation)
        {
            this.newsService = newsService;
        }

        public async Task Initialize(string scope) => await Initialize(scope.ToLower() switch
        {
            "local" => NewsScope.Local,
            "global" => NewsScope.Global,
            "headlines" => NewsScope.Headlines,
            "dotnetmaui" => NewsScope.DotnetMaui,
            _ => NewsScope.Headlines
        });

        public async Task Initialize(NewsScope scope)
        {
            CurrentNews = await newsService.GetNews(scope);
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

