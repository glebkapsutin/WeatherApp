using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using WeatherAPP.ViewModels;

namespace WeatherAPP.ViewModels
{
    public class TemperatureUnit
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
    }

    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly IPreferences _preferences;
        private readonly WeatherUImodel _weatherModel;
        private string _appVersion = string.Empty;
        private bool _isDarkTheme;
        private bool _isCelsius = true;

        public ObservableCollection<TemperatureUnit> TemperatureUnits { get; } = new()
        {
            new TemperatureUnit { Name = "Цельсий", Value = "C", Symbol = "°C" },
            new TemperatureUnit { Name = "Фаренгейт", Value = "F", Symbol = "°F" },
            new TemperatureUnit { Name = "Кельвин", Value = "K", Symbol = "K" }
        };

        public bool IsCelsius
        {
            get => _isCelsius;
            set
            {
                if (_isCelsius != value)
                {
                    _isCelsius = value;
                    _preferences.Set("temperature_unit", value ? "C" : "F");
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsFahrenheit));
                    
                    // Обновляем данные погоды при смене единиц измерения
                    RefreshWeatherData();
                }
            }
        }

        public bool IsFahrenheit
        {
            get => !_isCelsius;
            set
            {
                if (!_isCelsius != value)
                {
                    _isCelsius = !value;
                    _preferences.Set("temperature_unit", value ? "F" : "C");
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsCelsius));
                    
                    // Обновляем данные погоды при смене единиц измерения
                    RefreshWeatherData();
                }
            }
        }

        public string AppVersion
        {
            get => _appVersion;
            set
            {
                if (_appVersion != value)
                {
                    _appVersion = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (_isDarkTheme != value)
                {
                    _isDarkTheme = value;
                    _preferences.Set("is_dark_theme", value);
                    OnPropertyChanged();
                    Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public SettingsViewModel()
        {
            _preferences = Preferences.Default;
            
            // Получаем экземпляр WeatherUImodel через DI
            _weatherModel = Application.Current.Handler.MauiContext.Services.GetService<WeatherUImodel>();
            
            LoadSettings();
        }

        private void LoadSettings()
        {
            IsCelsius = _preferences.Get("temperature_unit", "C") == "C";
            IsDarkTheme = _preferences.Get("is_dark_theme", false);
            AppVersion = AppInfo.Current.VersionString;
        }
        
        private void RefreshWeatherData()
        {
            // Если модель существует, обновляем данные
            if (_weatherModel != null)
            {
                _weatherModel.RefreshWeatherDisplay();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 