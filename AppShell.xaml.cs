using WeatherAPP.Views;

namespace WeatherAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Регистрируем маршруты для навигации
            Routing.RegisterRoute(nameof(CurrentWeatherPage), typeof(CurrentWeatherPage));
            Routing.RegisterRoute(nameof(ForecastPage), typeof(ForecastPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }
    }
}
