using System;
using MauiNewsApp2.Models;

namespace MauiNewsApp2.Services
{
	public interface IDBService
	{
		Task<List<Article>> GetArticlesAsync();
		Task<Article> GetArticleByUrlAsync(string url);
        Task<int> SaveArticleAsync(Article article);
		Task<int> DeleteArticleAsync(Article article);

        Task<List<Source>> GetSourcesAsync();
        Task<int> SaveSourceAsync(Source source);
    }
}

