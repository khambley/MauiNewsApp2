using System;
using MauiNewsApp2.ViewModels;

namespace MauiNewsApp2
{
	public class Navigator : INavigate
	{
		public Navigator()
		{
		}

        public async Task NavigateTo(string route) => await Shell.Current.GoToAsync(route);

        public async Task PopModal() => await Shell.Current.Navigation.PopModalAsync();

        public async Task PushModal(Page page) => await Shell.Current.Navigation.PushModalAsync(page);

    }
}

