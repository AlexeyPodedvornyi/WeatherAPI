using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeatherAPI.Models;
using WeatherAPI.Services.Interfaces;
using System.Net.Http.Formatting;
using Swagger.Net.Annotations;
using System.Net;
using Swashbuckle.AspNetCore.Filters;
using WeatherAPI.Models.Responses;
using Newtonsoft.Json;
using WeatherAPI.Serializer.Interfaces;
using WeatherAPI.Models.Responses.Interfaces;
using WeatherAPI.Factories.Interfaces;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/weather_forecast")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly IResponseModelSerializer<IResponseModel> _responseModelSerializer;
        private readonly IErrorResponseModelFactory _errorResponseModelFactory;

        public WeatherForecastController(IWeatherForecastService weatherForecastService, IResponseModelSerializer<IResponseModel> responseModelSerializer,
            IErrorResponseModelFactory errorResponseModelFactory)
        {
            _weatherForecastService = weatherForecastService;
            _responseModelSerializer = responseModelSerializer;
            _errorResponseModelFactory = errorResponseModelFactory;
        }

            /// <summary>
        /// Get the current weather forecast for entired city.
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
        [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseModel400Example))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseModel404Example))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseModel500Example))]
        public async Task<IActionResult> GetCurrentWeatherByCity([Required] string cityName)
        {
            try
            {
                var result = await _weatherForecastService.GetCurrentWeatherAsync(cityName);
                if(result.Item1 != HttpStatusCode.OK)
                {
                    var error = _errorResponseModelFactory.CreateErrorResponse(result.Item1);

                    return _responseModelSerializer.ToJson(error);
                }
                return StatusCode((int)result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                var error = new ErrorResponseModel500Example(ex.Message);
                return _responseModelSerializer.ToJson(error);
            }
        }

        /// <summary>
        /// Get the weather forecast for 5 days with interval 3 hours for entired city.
        /// </summary>
        /// <param name="cityName">The city name</param>
        /// 
        /// <returns>A weather forecast for 5 days</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/weather_forecast/forecast?cityName=Dnipro
        ///     [{"date":"2023-07-09T18:00:00","temp":19.89,"temp_min":18.63,"temp_max":19.89,"wind_speed":3.08,"clouds":100},
        ///     {"date":"2023-07-09T21:00:00","temp":18.97,"temp_min":18.19,"temp_max":18.97,"wind_speed":5.55,"clouds":100},
        ///     {"date":"2023-07-10T00:00:00","temp":16.1,"temp_min":16.1,"temp_max":16.1,"wind_speed":5.09,"clouds":99},
        ///     {"date":"2023-07-10T03:00:00","temp":15.47,"temp_min":15.47,"temp_max":15.47,"wind_speed":5.27,"clouds":87},
        ///     {"date":"2023-07-10T06:00:00","temp":20.05,"temp_min":20.05,"temp_max":20.05,"wind_speed":6.41,"clouds":59},
        ///     .......
        ///     ]
        /// </remarks>
        /// <response code="200">Returns the weather forecast for 5 days in the specified city</response>
        /// <response code="400">If the cityName is null</response>
        /// <response code="404">The specified city doesn`t exist</response>
        /// <response code="500">Internal error, like internal parsing error, etc.</response>
        [HttpGet("forecast")]
        [ProducesResponseType(typeof(List<WeatherForecast>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseModel400Example))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseModel404Example))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseModel500Example))]
        public async Task<IActionResult> GetFiveDaysForecast(string cityName)
        {
            try
            {
                var result = await _weatherForecastService.GetWeatherForFiveDaysAsync(cityName);
                if (result.Item1 != HttpStatusCode.OK)
                {
                    var error = _errorResponseModelFactory.CreateErrorResponse(result.Item1);

                    return _responseModelSerializer.ToJson(error);
                }
                return StatusCode((int)result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                var error = new ErrorResponseModel500Example(ex.Message);
                return _responseModelSerializer.ToJson(error);
            }
        }
    }
}