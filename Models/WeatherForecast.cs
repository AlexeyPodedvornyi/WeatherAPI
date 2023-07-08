using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.Models
{
    public class WeatherForecast
    {
        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("temp")]
        public double? Temperature { get; set; }

        [JsonProperty("temp_min")]
        public double? TemperatureMin { get; set; }

        [JsonProperty("temp_max")]
        public double? TemperatureMax { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("clouds")]
        public int Ñlouds { get; set; }
    }
}