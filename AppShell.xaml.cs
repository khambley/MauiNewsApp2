namespace MauiNewsApp2;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("articleview", typeof(Pages.ArticlePage));
	}
}

