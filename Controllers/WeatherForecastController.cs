using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet("{cityName}")]
        public async Task<ActionResult<string>> GetCurrentWeatherByCity(string cityName)
        {
            try
            {
                if (string.IsNullOrEmpty(cityName))
                {
                    return BadRequest("'cityName' cannot be empty");
                }

                string weatherData = await _weatherForecastService.GetCurrentWeatherAsync(cityName);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
    }
}