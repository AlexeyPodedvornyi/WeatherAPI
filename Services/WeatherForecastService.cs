using WeatherAPI.Parsers.Interfaces;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private const string _apiKey = "2608faa819625201da6b8adb68cf47f1";
        private readonly IJsonParser _jsonParser;
        private readonly HttpClient _httpClient;
        public WeatherForecastService(IJsonParser jsonParser, HttpClient httpClient) 
        {
            _jsonParser = jsonParser;
            _httpClient = httpClient;
        }

        public async Task<string> GetCurrentWeatherByCityAsync(string cityName)
        {
          

        }
    }
}
