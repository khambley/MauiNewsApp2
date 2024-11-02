using System;
using MauiNewsApp2.Models;

namespace MauiNewsApp2.Services
{
	public interface INewsService
	{
		public Task<NewsResult> GetNews(NewsScope scope);
	}
}

