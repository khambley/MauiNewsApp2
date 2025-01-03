using System;
using MauiNewsApp2.Services;

namespace MauiNewsApp2.ViewModels
{
	public class FavoritesViewModel : ViewModelBase
	{
        private readonly IDBService dbService;
        private readonly INewsService newsService;

        public FavoritesViewModel(INewsService newsService, IDBService dbService, INavigate navigation) :base(navigation)
		{
            this.dbService = dbService;
            this.newsService = newsService;
        }
	}
}

