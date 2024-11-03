using System;
using System.Net.Http.Json;
using MauiNewsApp2.Models;

namespace MauiNewsApp2.Services
{
	public class NewsService : INewsService, IDisposable
	{
        private bool disposedValue;

        const string UriBase = "https://newsapi.org/v2";

        readonly HttpClient httpClient = new()
        {
            BaseAddress = new(UriBase),
            DefaultRequestHeaders = { { "user-agent", "mauiprojects-news/1.0" } }
        };

        private static string Headlines => $"{UriBase}/top-headlines?country=us&pageSize=100&apiKey={AppSettings.NewsApiKey}";

        private static string Local => $"{UriBase}/everything?q=local&apiKey={AppSettings.NewsApiKey}";

        private static string Global => $"{UriBase}/everything?q=global&apiKey={AppSettings.NewsApiKey}";

        private static string DotnetMaui => $"{UriBase}/everything?q=.NET%20Maui&apiKey={AppSettings.NewsApiKey}";

        public NewsService()
		{

		}



        public async Task<NewsResult> GetNews(NewsScope scope)
        {
            NewsResult result;
            string url = GetUrl(scope);
            try
            {
                result = await httpClient.GetFromJsonAsync<NewsResult>(url);
            }
            catch (Exception ex)
            {
                result = new()
                {
                    Articles = new() { new() { Title = $"HTTP Get failed: {ex.Message}", PublishedAt = DateTime.Now } }
                };
            }
            var orderedArticles = result.Articles.OrderByDescending(x => x.PublishedAt).ToList();
            result.Articles = orderedArticles;
            return result;
        }
        private string GetUrl(NewsScope scope) => scope switch
        {
            NewsScope.Headlines => Headlines,
            NewsScope.Global => Global,
            NewsScope.Local => Local,
            NewsScope.DotnetMaui => DotnetMaui,
            _ => throw new Exception("Undefined scope")
        };
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    httpClient.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

