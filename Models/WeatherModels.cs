using Newtonsoft.Json;

namespace WeatherAPP.Models
{
    public class CurrentWeatherAPI
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }
    }

    public class WeatherForecast
    {
        [JsonProperty("forecast")]
        public Forecast Forecast { get; set; }
    }

    public class Forecast
    {
        [JsonProperty("forecastday")]
        public List<ForecastDay> ForecastDays { get; set; }
    }

    public class ForecastDay
    {
        private DateTime _date;

        [JsonProperty("date")]
        public string DateString { get; set; }

        [JsonIgnore]
        public DateTime Date
        {
            get
            {
                if (_date == default && !string.IsNullOrEmpty(DateString))
                {
                    if (DateTime.TryParse(DateString, out var parsedDate))
                    {
                        _date = parsedDate;
                    }
                }
                return _date;
            }
            set => _date = value;
        }

        [JsonProperty("day")]
        public Day Day { get; set; }
    }

    public class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("localtime")]
        public string Localtime { get; set; }
    }

    public class Current
    {
        [JsonProperty("temp_c")]
        public double Temp_C { get; set; }

        [JsonProperty("condition")]
        public Condition Condition { get; set; }

        [JsonProperty("wind_kph")]
        public double Wind_Kph { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("feelslike_c")]
        public double Feelslike_C { get; set; }

        [JsonProperty("cloud")]
        public double Cloud { get; set; }
    }

    public class Condition
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class Day
    {
        [JsonProperty("maxtemp_c")]
        public double MaxTemp_C { get; set; }

        [JsonProperty("mintemp_c")]
        public double MinTemp_C { get; set; }

        [JsonProperty("avgtemp_c")]
        public double AvgTemp_C { get; set; }

        [JsonProperty("maxwind_kph")]
        public double MaxWind_Kph { get; set; }

        [JsonProperty("minwind_kph")]
        public double MinWind_Kph { get; set; }

        [JsonProperty("avghumidity")]
        public double Avghumidity { get; set; }

        [JsonProperty("totalprecip_mm")]
        public double Totalprecip_Mm { get; set; }

        [JsonProperty("condition")]
        public Condition Condition { get; set; }
    }
} 