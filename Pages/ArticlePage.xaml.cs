namespace MauiNewsApp2.Pages;
using System.Web;

[QueryProperty("Url", "url")]
[QueryProperty("Title", "title")]
public partial class ArticlePage : ContentPage
{
	public new string Title { get; set; }
	public string WebLink { get; set; }
	public string Url
	{
		set
		{
			WebLink = value;
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

    async void ShareButton_Clicked(System.Object sender, System.EventArgs e)
    {
		await Share.Default.RequestAsync(new ShareTextRequest
		{
			Uri = WebLink,
			Title = Title,
			Subject = Title
		}); ;
    }
}
