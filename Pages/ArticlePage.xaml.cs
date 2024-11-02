namespace MauiNewsApp2.Pages;
using System.Web;

[QueryProperty("Url", "url")]
public partial class ArticlePage : ContentPage
{
	public string Url
	{
		set
		{
			BindingContext = new UrlWebViewSource
			{
				Url = HttpUtility.UrlDecode(value)
			};
		}
	}
	public ArticlePage()
	{
		InitializeComponent();
	}
}
