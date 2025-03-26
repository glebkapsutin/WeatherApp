using WeatherAPP.ViewModels;

namespace WeatherAPP.Views;

public partial class ForecastPage : ContentPage
{
    public ForecastPage(WeatherUImodel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
} 