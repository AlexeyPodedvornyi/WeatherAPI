using Newtonsoft.Json;
using WeatherAPI.Exceptions;
using WeatherAPI.Models;
using WeatherAPI.Parsers.Interfaces;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private const string _apiKey = "2608faa819625201da6b8adb68cf47f1";
        private readonly IWeatherDataParser _weatherDataParser;
        private readonly HttpClient _httpClient;
        public WeatherForecastService(IWeatherDataParser weatherDataParser, HttpClient httpClient) 
        {
            _weatherDataParser = weatherDataParser;
            _httpClient = httpClient;
        }

        public async Task<string> GetCurrentWeatherAsync(string cityName)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Error when requesting the OpenWeatherMap API");
            }
        
            WeatherForecast parsedObject;
            try
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                parsedObject = _weatherDataParser.ParseFrom(jsonString);
            }
            catch (Exception ex)
            {
                throw new WeatherDataParsingException("Error in weather forecast parsing", ex);
            }

            return _weatherDataParser.ParseTo(parsedObject);
        }
    }
}
