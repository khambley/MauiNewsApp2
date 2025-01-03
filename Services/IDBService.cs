using System;
using MauiNewsApp2.Models;

namespace MauiNewsApp2.Services
{
	public interface IDBService
	{
		Task<List<Article>> GetArticlesAsync();
		Task<Article> GetArticleByUrlAsync(string url);
        Task SaveArticleAsync(Article article);
		Task<int> DeleteArticleAsync(Article article);

        Task<List<Source>> GetSourcesAsync();
        Task SaveSourceAsync(Source source);

		Task UpdateArticleAsync(Article article);
		Task UpdateSourceAsync(Source source);
    }
}

