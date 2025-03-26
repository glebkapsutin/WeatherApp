using WeatherAPP.ViewModels;

namespace WeatherAPP.Views;

public partial class CurrentWeatherPage : ContentPage
{
    public CurrentWeatherPage(WeatherUImodel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
} 