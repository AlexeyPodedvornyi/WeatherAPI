using dotenv.net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WeatherAPI.Exceptions;
using WeatherAPI.Models;
using WeatherAPI.Parsers.Interfaces;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY");
        private readonly IWeatherDataParser _weatherDataParser;
        private readonly HttpClient _httpClient;
        public WeatherForecastService(IWeatherDataParser weatherDataParser, HttpClient httpClient) 
        {
            _weatherDataParser = weatherDataParser;
            _httpClient = httpClient;
        }

        public async Task<(HttpStatusCode, string)> GetCurrentWeatherAsync(string cityName)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid={_apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return (response.StatusCode, responseContent);

            }
        
            WeatherForecast parsedObject;
            try
            {
                parsedObject = _weatherDataParser.ParseFromObject(responseContent);               

            }
            catch (Exception exception)
            {
                throw new WeatherDataParsingException("Error in weather forecast parsing", exception);
            }

            return (response.StatusCode, _weatherDataParser.ParseToJson(parsedObject));
        }

        public async Task<(HttpStatusCode, string)> GetWeatherForFiveDaysAsync(string cityName)
        {        
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&units=metric&appid={_apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return (response.StatusCode, responseContent);

            }

            List<WeatherForecast> parsedList;
            try
            {
                parsedList = _weatherDataParser.ParseFromArray(responseContent);
            }
            catch (Exception exception)
            {
                throw new WeatherDataParsingException("Error in weather forecast parsing", exception);
            }

            return (response.StatusCode, _weatherDataParser.ParseToJson(parsedList));          
        }
    }
}
