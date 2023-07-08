using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherAPI.Models;
using WeatherAPI.Parsers.Interfaces;

namespace WeatherAPI.Parsers
{
    public class WeatherDataParser : IWeatherDataParser
    {
        public List<WeatherForecast> ParseFromArray(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            var jsonObject = JObject.Parse(data);           

            List<WeatherForecast> forecasts = new List<WeatherForecast>();
            var jToken = jsonObject?["list"];
            if (jToken?.Type == JTokenType.Array)
            {
                var jArray = (JArray)jToken;
                foreach(var item in jArray)
                {
                    var forecast = ParseFromObject(item.ToString());
                    forecast.Date = item?["dt_txt"]?.Value<DateTime>();
                    forecasts.Add(forecast);
                }
            }
            else
            {
                throw new JsonException("The searched key in the object is missing");
            }

            return forecasts;
        }

        public WeatherForecast ParseFromObject(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            var jsonObject = JObject.Parse(data);

            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Now,
                Temperature = jsonObject?["main"]?["temp"]?.Value<double>(),
                TemperatureMin = jsonObject?["main"]?["temp_min"]?.Value<double>(),
                TemperatureMax = jsonObject?["main"]?["temp_max"]?.Value<double>(),
                WindSpeed = jsonObject?["wind"]?["speed"]?.Value<double>() ?? 0,
                Сlouds = jsonObject?["clouds"]?["all"]?.Value<int>() ?? 0,
            };

            return weatherForecast;
        }

        public string ParseToJson(WeatherForecast data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return JsonConvert.SerializeObject(data);
        }
        public string ParseToJson(List<WeatherForecast> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return JsonConvert.SerializeObject(data);
        }
    }
}
