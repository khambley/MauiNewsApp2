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
        private readonly IDBService dbService;
        private readonly INewsService newsService;

        [ObservableProperty]
        private NewsResult currentNews;

        public HeadlinesViewModel(INewsService newsService, IDBService dbService, INavigate navigation) : base (navigation)
        {
            this.dbService = dbService;
            this.newsService = newsService;
            CurrentNews = new NewsResult();
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
            //CurrentNews = await newsService.GetNews(scope);

            List<Article> dbArticles = new();

            // Fetch articles from newsapi.org
            var apiNews = await newsService.GetNews(scope);
            foreach (var apiArticle in apiNews.Articles)
            {
                // Map API article to Article model
                var article = new Article
                {
                    Url = apiArticle.Url,
                    Author = apiArticle.Author,
                    Title = apiArticle.Title,
                    Description = apiArticle.Description,
                    UrlToImage = apiArticle.UrlToImage,
                    PublishedAt = apiArticle.PublishedAt,
                    Content = apiArticle.Content,
                    Timestamp = DateTime.UtcNow.Date
                };

                // handle the fk constraint - Source
                if(apiArticle.Source != null)
                {
                    var sources = await dbService.GetSourcesAsync();
                    if(sources != null)
                    {
                        // check to see if article's source already exists in db.
                        var dbSource = sources.Where(n => n.Name == apiArticle.Source.Name).FirstOrDefault();

                        if(dbSource != null)
                        {
                            // Map API article's source to article model
                            article.SourceId = dbSource.SourceId;
                            article.Source = new Source
                            {
                                Id = dbSource.Id ?? null,
                                Name = dbSource.Name ?? "Unknown"
                            };

                        }
                        else
                        {
                            // save new source in db
                            await dbService.SaveSourceAsync(apiArticle.Source);

                            // get new source id from db
                            sources = await dbService.GetSourcesAsync();
                            dbSource = sources.Where(n => n.Name == apiArticle.Source.Name).FirstOrDefault();

                            // set the article source Id to the new db source id
                            if(dbSource != null)
                            {
                                // Map API article's source to article model
                                article.SourceId = dbSource.SourceId;
                                article.Source = new Source
                                {
                                    Id = dbSource.Id ?? null,
                                    Name = dbSource.Name ?? "Unknown"
                                };
                            }
                        }
                    }
                }

                // Check if the article exists in the database
                var existingArticle = await dbService.GetArticleByUrlAsync(article.Url);
                if(existingArticle != null)
                {
                    //Update existing article
                    existingArticle.IsFavorite = article.IsFavorite;
                    existingArticle.Timestamp = DateTime.UtcNow;
                    existingArticle.SourceId = article.SourceId;
                    existingArticle.Source = new Source
                    {
                        Id = article.Source.Id ?? null,
                        Name = article.Source.Name ?? "Unknown"
                    };
                    existingArticle.Author = article.Author;
                    existingArticle.Title = article.Title;
                    existingArticle.Description = article.Description;
                    existingArticle.UrlToImage = article.UrlToImage;
                    existingArticle.PublishedAt = article.PublishedAt;
                    existingArticle.Content = article.Content;

                    await dbService.SaveArticleAsync(existingArticle);
                    dbArticles.Add(existingArticle);
                }
                else
                {
                    // Insert new article
                    await dbService.SaveArticleAsync(article);
                    dbArticles.Add(article);
                }
            }

            // Set the api articles retrieved from the updated db.
            apiNews.Articles = dbArticles;

            // Set the current news result properties with the db articles.
            CurrentNews = apiNews;

        }

        [RelayCommand]
        private async Task ToggleFavorite(Article article)
        {
            if (article != null)
            {
                if (article.IsFavorite == false)
                {
                    article.IsFavorite = true;
                }
                else if (article.IsFavorite == true)
                {
                    article.IsFavorite = false;
                }
                await dbService.SaveArticleAsync(article);

            }
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

