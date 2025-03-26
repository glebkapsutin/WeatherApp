using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;

namespace WeatherAPP.ViewModels
{
    public class ThemeOption
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class TemperatureUnit
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class SettingsViewModel : INotifyPropertyChanged
    {
        private ThemeOption _selectedTheme = new();
        private TemperatureUnit _selectedTemperatureUnit = new();
        private bool _notificationsEnabled;
        private bool _rainNotificationsEnabled;
        private string _appVersion = string.Empty;
        private bool _isDarkTheme;
        private string _selectedLanguage = string.Empty;

        public ObservableCollection<ThemeOption> Themes { get; } = new()
        {
            new ThemeOption { Name = "Светлая", Value = "Light" },
            new ThemeOption { Name = "Темная", Value = "Dark" },
            new ThemeOption { Name = "Системная", Value = "System" }
        };

        public ObservableCollection<TemperatureUnit> TemperatureUnits { get; } = new()
        {
            new TemperatureUnit { Name = "Цельсий", Value = "C" },
            new TemperatureUnit { Name = "Фаренгейт", Value = "F" }
        };

        public ThemeOption SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged();
                    // Здесь можно добавить логику изменения темы
                }
            }
        }

        public TemperatureUnit SelectedTemperatureUnit
        {
            get => _selectedTemperatureUnit;
            set
            {
                if (_selectedTemperatureUnit != value)
                {
                    _selectedTemperatureUnit = value;
                    OnPropertyChanged();
                    // Здесь можно добавить логику изменения единиц измерения
                }
            }
        }

        public bool NotificationsEnabled
        {
            get => _notificationsEnabled;
            set
            {
                if (_notificationsEnabled != value)
                {
                    _notificationsEnabled = value;
                    OnPropertyChanged();
                    // Здесь можно добавить логику включения/выключения уведомлений
                }
            }
        }

        public bool RainNotificationsEnabled
        {
            get => _rainNotificationsEnabled;
            set
            {
                if (_rainNotificationsEnabled != value)
                {
                    _rainNotificationsEnabled = value;
                    OnPropertyChanged();
                    // Здесь можно добавить логику включения/выключения уведомлений о дожде
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
                    Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    // Здесь будет логика изменения языка
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public SettingsViewModel()
        {
            // Загрузка сохраненных настроек
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Здесь будет загрузка настроек из хранилища
            SelectedTheme = Themes.FirstOrDefault(t => t.Value == "System") ?? Themes[0];
            SelectedTemperatureUnit = TemperatureUnits[0];
            AppVersion = "1.0.0";
            IsDarkTheme = Application.Current.UserAppTheme == AppTheme.Dark;
            SelectedLanguage = "ru"; // По умолчанию русский
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 