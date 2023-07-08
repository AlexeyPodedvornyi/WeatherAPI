using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WeatherAPI.Services.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<(HttpStatusCode, string)> GetCurrentWeatherAsync(string cityName);
        Task<(HttpStatusCode, string)> GetWeatherForFiveDaysAsync(string cityName);
    }
}
