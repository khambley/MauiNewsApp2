using System;
using MauiNewsApp2.Models;
using SQLite;

namespace MauiNewsApp2.Services
{
	public class DBService : IDBService
	{
        private SQLiteAsyncConnection _database;

        private async Task CreateConnectionAsync()
        {
            if (_database != null) return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "newsapp.db");

            _database = new SQLiteAsyncConnection(dbPath);
            try
            {
                await _database.CreateTableAsync<Source>();
                await _database.CreateTableAsync<Article>();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error Creating Table:\n\n{ex.Message}", "OK");
            }

            if(await _database.Table<Source>().CountAsync() == 0)
            {
                await _database.InsertAsync(new Source()
                {
                    Id = "SourceExampleId",
                    Name = "SourceExampleName"

                });
            }

            if (await _database.Table<Article>().CountAsync() == 0)
            {
                await _database.InsertAsync(new Article()
                {
                    Url = "https://www.superdesigngirl.com",
                    Title = "Welcome to MyNews",
                    UrlToImage = "https://www.superdesigngirl.com/assets/img/about.jpg",
                    Timestamp = DateTime.UtcNow,
                    PublishedAt = DateTime.UtcNow,
                    
                });
            }
        }

        #region Article

        //returns the number of rows deleted
        public async Task<int> DeleteArticleAsync(Article article)
        {
            await CreateConnectionAsync();
            return await _database.DeleteAsync(article);
        }
       
        public async Task<Article> GetArticleByUrlAsync(string url)
        {
            await CreateConnectionAsync();
            return await _database.Table<Article>()
                .Where(a => a.Url == url)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Article>> GetArticlesAsync()
        {
            await CreateConnectionAsync();

            // map source to article with source id
            var sources = await GetSourcesAsync();
            var articles = await _database.Table<Article>().ToListAsync();
            List<Article> articlesWithSources = new();

            if(sources != null)
            {
                foreach (var article in articles)
                {
                    article.Source = await GetSourceById(article.SourceId);
                    articlesWithSources.Add(article);

                }
            }        
            return articlesWithSources.OrderByDescending(a => a.PublishedAt).ToList();
        }
        
        public async Task SaveArticleAsync(Article article)
        {
            await CreateConnectionAsync();
            await _database.InsertAsync(article);
        }

        public async Task UpdateArticleAsync(Article article){
            await CreateConnectionAsync();
            await _database.UpdateAsync(article);
        }

        #endregion

        #region Source

        public async Task<List<Source>> GetSourcesAsync()
        {
            await CreateConnectionAsync();
            return await _database.Table<Source>().ToListAsync();
        }

        public async Task<Source> GetSourceById(int id)
        {
            await CreateConnectionAsync();
            return await _database.Table<Source>()
                .Where(s => s.SourceId == id)
                .FirstOrDefaultAsync();
        }

        public async Task SaveSourceAsync(Source source)
        {
            await CreateConnectionAsync();
            await _database.InsertAsync(source);
        }
        
        public async Task UpdateSourceAsync(Source source)
        {
            await CreateConnectionAsync();
            await _database.UpdateAsync(source);
        }

        #endregion
    }
}

