using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherAPP.Animations;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using WeatherAPP.Services;
using WeatherAPP.Models;
using Microsoft.Maui.Storage;

namespace WeatherAPP.ViewModels
{
    public class WeatherUImodel : INotifyPropertyChanged
    {
        private readonly WeatherService _weatherService;
        private readonly IPreferences _preferences;
        private Color _backgroundColor = Colors.LightGray;
        private Color _textColor = Colors.Black;
        private Color _cardBackgroundColor = Colors.White;

        private GraphicsView? _weatherAnim;
        private string _cityName = string.Empty;
        private string _temperature = string.Empty;
        private string _description = string.Empty;
        private string _icon = string.Empty;
        private string _windKph = string.Empty;
        private string _cloud = string.Empty;
        private string _humidity = string.Empty;
        private string _feelsLike = string.Empty;

        private string _errorMessage = string.Empty;
        private bool _isErrorVisible;

        public ICommand GetWeatherCommand { get; }
        public ICommand GetForecastCommand { get; }
        public ObservableCollection<ForecastDay> ForecastDays { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public WeatherUImodel()
        {
            _weatherService = new WeatherService();
            _preferences = Preferences.Default;
            GetWeatherCommand = new Command(async () => await GetWeatherAsync());
            GetForecastCommand = new Command(async () => await GetForecastWeatherAsync());
            ForecastDays = new ObservableCollection<ForecastDay>();
            UpdateView();
        }

        // Метод для конвертации температуры из Цельсия в Фаренгейты
        private string ConvertTemperature(double tempCelsius)
        {
            string unit = _preferences.Get("temperature_unit", "C");
            string symbol = unit == "C" ? "°C" : "°F";
            
            if (unit == "C")
            {
                return $"{tempCelsius:F1}{symbol}";
            }
            else
            {
                // Формула для конвертации из Цельсия в Фаренгейты: (C × 9/5) + 32
                double tempFahrenheit = (tempCelsius * 9 / 5) + 32;
                return $"{tempFahrenheit:F1}{symbol}";
            }
        }

        // Метод для конвертации температуры с добавлением префикса
        private string ConvertTemperatureWithPrefix(double tempCelsius, string prefix)
        {
            string unit = _preferences.Get("temperature_unit", "C");
            string symbol = unit == "C" ? "°C" : "°F";
            
            if (unit == "C")
            {
                return $"{prefix} {tempCelsius:F1}{symbol}";
            }
            else
            {
                // Формула для конвертации из Цельсия в Фаренгейты: (C × 9/5) + 32
                double tempFahrenheit = (tempCelsius * 9 / 5) + 32;
                return $"{prefix} {tempFahrenheit:F1}{symbol}";
            }
        }

        private void UpdateView(string? condition = null)
        {
            if (string.IsNullOrEmpty(condition))
            {
                BackgroundColor = Colors.LightGray;
                TextColor = Colors.Black;
                CardBackgroundColor = Colors.White;
                WeatherAnim = null;
                return;
            }

            var hour = DateTime.Now.Hour;
            var isDarkTheme = Application.Current.UserAppTheme == AppTheme.Dark;

            if (isDarkTheme)
            {
                TextColor = Colors.White;
                CardBackgroundColor = Colors.Gray;
            }
            else
            {
                TextColor = Colors.Black;
                CardBackgroundColor = Colors.White;
            }

            if (hour >= 5 && hour < 20)
            {
                // Дневное время
                if (condition.IndexOf("Rain", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    BackgroundColor = Colors.Gray;
                    WeatherAnim = new RainAnimation();
                }
                else if (condition.IndexOf("Clear", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (hour >= 20 || hour < 6)
                    {
                        BackgroundColor = Colors.Black;
                        WeatherAnim = new NightAnimation();
                    }
                    else
                    {
                        BackgroundColor = Colors.LightBlue;
                        WeatherAnim = new SunAnimation();
                    }
                }
                else if (condition.IndexOf("Sunny", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    BackgroundColor = Colors.LightYellow;
                    WeatherAnim = new SunAnimation();
                }
                else
                {
                    BackgroundColor = Colors.LightYellow;
                    WeatherAnim = null;
                }
            }
            else
            {
                // Ночное время
                BackgroundColor = Colors.Black;
                WeatherAnim = new NightAnimation();
            }
        }

        public Color TextColor
        {
            get => _textColor;
            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color CardBackgroundColor
        {
            get => _cardBackgroundColor;
            set
            {
                if (_cardBackgroundColor != value)
                {
                    _cardBackgroundColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public GraphicsView? WeatherAnim
        {
            get => _weatherAnim;
            set
            {
                _weatherAnim = value;
                OnPropertyChanged();
            }
        }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }
        }

        public string CityName
        {
            get => _cityName;
            set
            {
                _cityName = value;
                OnPropertyChanged();
            }
        }

        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged();
            }
        }

        public string WindKph
        {
            get => _windKph;
            set
            {
                _windKph = value;
                OnPropertyChanged();
            }
        }

        public string Cloud
        {
            get => _cloud;
            set
            {
                _cloud = value;
                OnPropertyChanged();
            }
        }

        public string FeelsLike
        {
            get => _feelsLike;
            set
            {
                _feelsLike = value;
                OnPropertyChanged();
            }
        }

        public string Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set
            {
                _isErrorVisible = value;
                OnPropertyChanged();
            }
        }

        private async Task GetWeatherAsync()
        {
            IsErrorVisible = false;
            ErrorMessage = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(CityName))
                {
                    IsErrorVisible = true;
                    ErrorMessage = "Введите название города.";
                    return;
                }

                var weather = await _weatherService.GetCurrentWeatherResponseAsync(CityName);

                if (weather?.Current?.Condition != null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Temperature = ConvertTemperature(weather.Current.Temp_C);
                        FeelsLike = ConvertTemperatureWithPrefix(weather.Current.Feelslike_C, "Ощущается как");
                        Humidity = $"Влажность {weather.Current.Humidity}%";
                        if (weather.Current.Wind_Kph > 10)
                        {
                            WindKph = $"Сильный ветер {weather.Current.Wind_Kph} км/ч";
                        }
                        else
                        {
                            WindKph = $"Слабый ветер {weather.Current.Wind_Kph} км/ч";
                        }
                        Cloud = $"Облачность {weather.Current.Cloud}%";
                        Description = weather.Current.Condition.Text;
                        Icon = $"https:{weather.Current.Condition.Icon}";
                        UpdateView(weather.Current.Condition.Text);
                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        IsErrorVisible = true;
                        ErrorMessage = "Не удалось получить данные о погоде. Проверьте подключение к интернету или правильность введенного названия города.";
                    });
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsErrorVisible = true;
                    ErrorMessage = $"Произошла ошибка: {ex.Message}";
                });
            }
        }

        private async Task GetForecastWeatherAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CityName))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        IsErrorVisible = true;
                        ErrorMessage = "Введите название города";
                    });
                    return;
                }

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsErrorVisible = false;
                    ErrorMessage = string.Empty;
                    // Очищаем коллекцию сразу, чтобы избежать проблем с UI
                    ForecastDays.Clear();
                });

                try
                {
                    var forecast = await _weatherService.GetForecastAsync(CityName);
                    if (forecast?.Forecast?.ForecastDays != null)
                    {
                        foreach (var day in forecast.Forecast.ForecastDays)
                        {
                            try
                            {
                                if (day?.Day?.Condition != null)
                                {
                                    MainThread.BeginInvokeOnMainThread(() =>
                                    {
                                        try
                                        {
                                            // Модифицируем отображаемые значения температуры
                                            if (day.Day != null)
                                            {
                                                var tempUnit = _preferences.Get("temperature_unit", "C");
                                                if (tempUnit == "F")
                                                {
                                                    // Применяем конвертацию, сохраняя исходные данные
                                                    day.Day.DisplayMaxTemp_C = (day.Day.MaxTemp_C * 9 / 5) + 32;
                                                    day.Day.DisplayMinTemp_C = (day.Day.MinTemp_C * 9 / 5) + 32;
                                                    day.Day.DisplayAvgTemp_C = (day.Day.AvgTemp_C * 9 / 5) + 32;
                                                    day.Day.DisplayUnit = "°F";
                                                }
                                                else
                                                {
                                                    // Используем исходные данные
                                                    day.Day.DisplayMaxTemp_C = day.Day.MaxTemp_C;
                                                    day.Day.DisplayMinTemp_C = day.Day.MinTemp_C;
                                                    day.Day.DisplayAvgTemp_C = day.Day.AvgTemp_C;
                                                    day.Day.DisplayUnit = "°C";
                                                }
                                            }
                                            ForecastDays.Add(day);
                                        }
                                        catch (Exception ex)
                                        {
                                            IsErrorVisible = true;
                                            ErrorMessage = $"Ошибка при добавлении дня в коллекцию: {ex.Message}";
                                        }
                                    });
                                }
                            }
                            catch (Exception ex)
                            {
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    IsErrorVisible = true;
                                    ErrorMessage = $"Ошибка при обработке дня: {ex.Message}";
                                });
                            }
                        }

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            if (ForecastDays.Count == 0)
                            {
                                IsErrorVisible = true;
                                ErrorMessage = "Нет данных о прогнозе погоды";
                            }
                        });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            IsErrorVisible = true;
                            ErrorMessage = "Не удалось получить прогноз погоды";
                        });
                    }
                }
                catch (Exception ex)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        IsErrorVisible = true;
                        ErrorMessage = $"Ошибка при получении прогноза: {ex.Message}";
                    });
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsErrorVisible = true;
                    ErrorMessage = $"Общая ошибка: {ex.Message}";
                });
            }
        }

        // Метод для обновления отображения погоды при изменении единиц измерения
        public void RefreshWeatherDisplay()
        {
            // Если температура уже была загружена, обновляем её формат
            if (ForecastDays.Count > 0)
            {
                // Обновляем прогноз
                foreach (var day in ForecastDays)
                {
                    if (day.Day != null)
                    {
                        string unit = _preferences.Get("temperature_unit", "C");
                        if (unit == "F")
                        {
                            day.Day.DisplayMaxTemp_C = (day.Day.MaxTemp_C * 9 / 5) + 32;
                            day.Day.DisplayMinTemp_C = (day.Day.MinTemp_C * 9 / 5) + 32;
                            day.Day.DisplayAvgTemp_C = (day.Day.AvgTemp_C * 9 / 5) + 32;
                            day.Day.DisplayUnit = "°F";
                        }
                        else
                        {
                            day.Day.DisplayMaxTemp_C = day.Day.MaxTemp_C;
                            day.Day.DisplayMinTemp_C = day.Day.MinTemp_C;
                            day.Day.DisplayAvgTemp_C = day.Day.AvgTemp_C;
                            day.Day.DisplayUnit = "°C";
                        }
                    }
                }

                // Уведомляем UI об изменениях
                OnPropertyChanged(nameof(ForecastDays));
            }

            // Обновляем текущую погоду, если она загружена
            if (!string.IsNullOrEmpty(_temperature) && !string.IsNullOrEmpty(_cityName))
            {
                // Получаем числовое значение из строки
                if (double.TryParse(_temperature.Replace("°C", "").Replace("°F", ""), out double tempValue))
                {
                    string unit = _preferences.Get("temperature_unit", "C");
                    
                    // Если сейчас в Фаренгейтах, но нужны Цельсии
                    if (_temperature.Contains("°F") && unit == "C")
                    {
                        // Формула для конвертации из Фаренгейта в Цельсии: (F - 32) * 5/9
                        double tempCelsius = (tempValue - 32) * 5 / 9;
                        Temperature = $"{tempCelsius:F1}°C";
                    }
                    // Если сейчас в Цельсиях, но нужны Фаренгейты
                    else if (_temperature.Contains("°C") && unit == "F")
                    {
                        // Формула для конвертации из Цельсия в Фаренгейты: (C × 9/5) + 32
                        double tempFahrenheit = (tempValue * 9 / 5) + 32;
                        Temperature = $"{tempFahrenheit:F1}°F";
                    }
                }

                // Аналогично для ощущаемой температуры
                if (_feelsLike.Contains("°C") || _feelsLike.Contains("°F"))
                {
                    string feelsTempStr = _feelsLike.Replace("Ощущается как ", "");
                    if (double.TryParse(feelsTempStr.Replace("°C", "").Replace("°F", ""), out double feelsValue))
                    {
                        string unit = _preferences.Get("temperature_unit", "C");
                        
                        // Если сейчас в Фаренгейтах, но нужны Цельсии
                        if (feelsTempStr.Contains("°F") && unit == "C")
                        {
                            // Формула для конвертации из Фаренгейта в Цельсии: (F - 32) * 5/9
                            double feelsCelsius = (feelsValue - 32) * 5 / 9;
                            FeelsLike = $"Ощущается как {feelsCelsius:F1}°C";
                        }
                        // Если сейчас в Цельсиях, но нужны Фаренгейты
                        else if (feelsTempStr.Contains("°C") && unit == "F")
                        {
                            // Формула для конвертации из Цельсия в Фаренгейты: (C × 9/5) + 32
                            double feelsFahrenheit = (feelsValue * 9 / 5) + 32;
                            FeelsLike = $"Ощущается как {feelsFahrenheit:F1}°F";
                        }
                    }
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
