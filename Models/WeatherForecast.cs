using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.Models
{
    public class WeatherForecast
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("temp_min")]
        public int? TemperatureMin { get; set; }

        [JsonProperty("temp_max")]
        public int? TemperatureMax { get; set; }

        [JsonProperty("wind_speed")]
        public double? WindSpeed { get; set; }

        [JsonProperty("cloudiness")]
        public string? Ñloudiness { get; set; }
    }
}