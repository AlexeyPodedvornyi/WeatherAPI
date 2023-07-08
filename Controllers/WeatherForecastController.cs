using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeatherAPI.Models;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/weather_forecast")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        /// <summary>
        /// Gets the current weather forecast for entired city.
        /// </summary>
        /// <param name="cityName">The city name</param>
        /// 
        /// <returns>A current weather</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/weather_forecast/current?cityName=Dnipro
        ///     {
        ///        "date":"2023-07-09T02:08:26.877504+03:00",
        ///        "temp":20.93,
        ///        "temp_min":20.19,
        ///        "temp_max":20.93,
        ///        "wind_speed":3.8,
        ///        "clouds":100
        ///     }
        /// </remarks>
        /// <response code="200">Returns the current weather in the specified city</response>
        /// <response code="400">If the cityName is null</response>
        /// <response code="404">The specified city doesn`t exist</response>
        /// <response code="500">Internal error, like internal parsing error, etc.</response>
        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseModel))]
        public async Task<ActionResult<string>> GetCurrentWeatherByCity([Required] string cityName)
        {
            try
            {
                var result = await _weatherForecastService.GetCurrentWeatherAsync(cityName);
                return StatusCode((int)result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

        [HttpGet("forecast")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseModel))]
        public async Task<ActionResult<string>> GetFiveDaysForecast(string cityName)
        {
            try
            {
                var result = await _weatherForecastService.GetWeatherForFiveDaysAsync(cityName);
                return StatusCode((int)result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
    }
}