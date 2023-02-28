using HashidsNet;
using Microsoft.Extensions.DependencyInjection;
using URLShortener.Models;
using URLShortener.Services;
using URLShortener.ViewModels;

namespace URLShortener;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.RegisterServices()
			.RegisterViewModels()
			.RegisterViews()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}

	public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<IRepository<UrlData>>(new DbRepository(Path.Combine(FileSystem.AppDataDirectory, "data.db3")));
		builder.Services.AddSingleton<IUrlShortener, CuttlyUrlShortener>();
		builder.Services.AddSingleton<IHashids, Hashids>();
		return builder;
	}

	public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
	{
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<AppShell>();
		return builder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<MainPageViewModel>();
		return builder;
	}
}
