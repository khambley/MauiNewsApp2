using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiNewsApp2.ViewModels
{
    public abstract partial class ViewModelBase : ObservableObject
    {
		public INavigate Navigation { get; init; }

		internal ViewModelBase(INavigate navigation) => Navigation = navigation;
		
	}
}

