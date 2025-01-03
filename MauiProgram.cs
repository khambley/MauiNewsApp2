using Microsoft.Extensions.Logging;

namespace MauiNewsApp2;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FontAwesome6Pro-Solid-900.otf", "FontAwesome");
            }).RegisterAppTypes();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
	public static MauiAppBuilder RegisterAppTypes(this MauiAppBuilder mauiAppBuilder)
	{
		//Services
		mauiAppBuilder.Services.AddSingleton<Services.INewsService>((serviceProvider) => new Services.NewsService());
		mauiAppBuilder.Services.AddSingleton<ViewModels.INavigate>((serviceProvider) => new Navigator());
		mauiAppBuilder.Services.AddSingleton<Services.IDBService, Services.DBService>();

		//ViewModels
		mauiAppBuilder.Services.AddTransient<ViewModels.HeadlinesViewModel>();
        mauiAppBuilder.Services.AddTransient<ViewModels.SearchViewModel>();
        mauiAppBuilder.Services.AddTransient<ViewModels.FavoritesViewModel>();

        //Pages
        mauiAppBuilder.Services.AddTransient<Pages.AboutPage>();
		mauiAppBuilder.Services.AddTransient<Pages.ArticlePage>();
		mauiAppBuilder.Services.AddTransient<Pages.HeadlinesPage>();
        mauiAppBuilder.Services.AddTransient<Pages.LocalNewsPage>();
        mauiAppBuilder.Services.AddTransient<Pages.GlobalNewsPage>();
        mauiAppBuilder.Services.AddTransient<Pages.SearchPage>();
        mauiAppBuilder.Services.AddTransient<Pages.FavoritesPage>();

        return mauiAppBuilder;
	}
}

