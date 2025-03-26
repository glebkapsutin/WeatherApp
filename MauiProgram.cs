using Microsoft.Extensions.Logging;
using WeatherAPP.Services;
using WeatherAPP.ViewModels;
using WeatherAPP.Views;

namespace WeatherAPP;

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
            });

        // Регистрируем сервисы
        builder.Services.AddSingleton<WeatherService>();
        
        // Регистрируем ViewModel как синглтон, чтобы использовать один экземпляр на всех страницах
        builder.Services.AddSingleton<WeatherUImodel>();
        
        // Регистрируем страницы
        builder.Services.AddTransient<CurrentWeatherPage>();
        builder.Services.AddTransient<ForecastPage>();
        builder.Services.AddTransient<SettingsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
