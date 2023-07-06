using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using WeatherAPI.Models;
using WeatherAPI.Parsers.Interfaces;

namespace WeatherAPI.Parsers
{
    public class WeatherDataParser: IJsonParser
    {
        public WeatherForecast ParseFrom(string data)
        {
            if(string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            var jsonObject = JObject.Parse(data);

            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Now.Date,
                TemperatureMin = Convert.ToInt32(jsonObject?["main"]?["temp_min"]),
                TemperatureMax = Convert.ToInt32(jsonObject?["main"]?["temp_max"]),
                WindSpeed = Convert.ToDouble(jsonObject?["wind"]?["speed"]),
                Сloudiness = jsonObject?["weather"]?["main"]?.ToString() ?? default
            };

            return weatherForecast;
        }

        public string ParseTo(WeatherForecast data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return JsonConvert.SerializeObject(data);
        }
    }
}
