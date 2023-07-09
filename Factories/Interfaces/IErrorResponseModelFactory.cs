using System.Net;
using WeatherAPI.Models.Responses;

namespace WeatherAPI.Factories.Interfaces
{
    public interface IErrorResponseModelFactory
    {
        ErrorResponseModel CreateErrorResponse(HttpStatusCode statusCode);
    }
}
