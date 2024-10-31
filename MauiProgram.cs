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
		//ViewModels
		mauiAppBuilder.Services.AddTransient<ViewModels.HeadlinesViewModel>();

		//Pages
		mauiAppBuilder.Services.AddTransient<Pages.AboutPage>();
		mauiAppBuilder.Services.AddTransient<Pages.ArticlePage>();
		mauiAppBuilder.Services.AddTransient<Pages.HeadlinesPage>();
		return mauiAppBuilder;
	}
}

