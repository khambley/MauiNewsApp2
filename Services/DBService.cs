using System;
using MauiNewsApp2.Models;
using SQLite;

namespace MauiNewsApp2.Services
{
	public class DBService : IDBService
	{
        private SQLiteAsyncConnection _database;

        public DBService()
		{

        }

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
                    Url = "https://www.google.com",
                    Title = "Welcome to JustNews",
                    //Timestamp = DateTime.UtcNow
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
            return await _database.Table<Article>().ToListAsync();
        }
        
        public async Task<int> SaveArticleAsync(Article article)
        {
            await CreateConnectionAsync();
            //returns the number of rows saved
            return await _database.InsertOrReplaceAsync(article);
        }

        #endregion

        #region Source

        public async Task<List<Source>> GetSourcesAsync()
        {
            await CreateConnectionAsync();
            return await _database.Table<Source>().ToListAsync();
        }

        public async Task<int> SaveSourceAsync(Source source)
        {
            await CreateConnectionAsync();
            //returns the number of rows saved
            return await _database.InsertOrReplaceAsync(source);
        }

        #endregion
    }
}

