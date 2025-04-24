# WeatherAPP - Кроссплатформенное Погодное Приложение

## Демо
![Demo](demo.mov) 
### P.S. после "Sorry about that, but we can’t show files that are this big right now" нажмите "View raw"
## 📱 Описание
WeatherAPP - это современное кроссплатформенное приложение для просмотра погоды, разработанное с использованием .NET MAUI. Приложение предоставляет актуальную информацию о погоде и прогноз на 7 дней для любого города мира. С красивым и интуитивно понятным интерфейсом, приложение демонстрирует возможности современной кроссплатформенной разработки.

## 🛠 Технологии
- .NET MAUI 8.0
- C# 12
- MVVM архитектура
- REST API (WeatherAPI)
- XAML для UI
- Асинхронное программирование
- Dependency Injection

## 🚀 Функциональность
- Поиск погоды по названию города
- Отображение текущей погоды
- Прогноз погоды на 7 дней
- Адаптивный дизайн для разных платформ
- Обработка ошибок и состояний загрузки

## ⚙️ Требования
- Visual Studio 2022 17.8 или новее с установленными компонентами:
  - .NET Multi-platform App UI development
  - Mobile development with .NET
  - Universal Windows Platform development
- .NET 8.0 SDK
- Для Android:
  - Android SDK
  - Android Emulator или физическое устройство
- Для Windows: Windows 10/11 (версия 10.0.19041.0 или выше)
- Для macOS: macOS 10.15 или выше
- Для iOS: macOS + Xcode 14.0 или выше
## 📦 Установка приложения (Windows)  

### Быстрый запуск  
Вы можете установить и запустить WeatherAPP без необходимости сборки исходников:  
1. Перейдите во вкладку **[Releases](https://github.com/glebkapsutin/WeatherApp/releases)**.  

---
## 🏗 Сборка и Запуск

### Подготовка проекта
1. Клонируйте репозиторий:
```bash
git clone https://github.com/glebkapsutin/WeatherApp.git
cd WeatherAPP
```

2. Восстановите зависимости:
```bash
dotnet restore

dotnet workload update
```

### Windows
1. Откройте решение `WeatherAPP.sln` в Visual Studio 2022
2. Выберите платформу Windows (x64)
3. Восстановите NuGet пакеты через Package Manager Console:
```powershell
Update-Package -reinstall
```
4. Соберите решение: Build -> Build Solution (Ctrl+Shift+B)
5. Запустите приложение: Debug -> Start Debugging (F5)

### Android
1. Откройте решение в Visual Studio 2022
2. Выберите платформу Android
3. Убедитесь, что Android SDK установлен и настроен
4. Восстановите NuGet пакеты
5. Подключите Android-устройство или запустите эмулятор
6. Соберите и запустите приложение (F5)

### macOS
1. Откройте решение в Visual Studio для Mac
2. Выберите платформу macOS
3. Восстановите NuGet пакеты
4. Соберите и запустите приложение (⌘+↵)

### iOS (требуется macOS)
1. Откройте решение в Visual Studio для Mac
2. Выберите платформу iOS
3. Восстановите NuGet пакеты
4. Подключите iOS устройство или запустите симулятор
5. Соберите и запустите приложение

### Запуск через командную строку
```bash
# Сборка для всех платформ
dotnet build

# Запуск для конкретной платформы
dotnet run -f net8.0-windows10.0.19041.0    # Windows
dotnet run -f net8.0-android                # Android
dotnet run -f net8.0-maccatalyst           # macOS
dotnet run -f net8.0-ios                    # iOS
```

## 💼 Почему этот проект?
Этот проект демонстрирует:
- Владение современными технологиями кроссплатформенной разработки
- Понимание принципов чистой архитектуры и MVVM
- Опыт работы с REST API и асинхронным программированием
- Навыки создания адаптивного пользовательского интерфейса
- Способность разрабатывать приложения для разных платформ с единой кодовой базой


## 💬 **Контакты**
👨‍💻 Автор: Kapustin Gleb
📧 Email: gleb.kapustin1998@gmail.com
🐙 GitHub: https://github.com/glebkapsutin
