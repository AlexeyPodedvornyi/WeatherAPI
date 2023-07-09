using System.Net;
using WeatherAPI.Factories.Interfaces;
using WeatherAPI.Models.Responses;

namespace WeatherAPI.Factories
{
    public class ErrorResponseModelFactory : IErrorResponseModelFactory
    {
        public ErrorResponseModel CreateErrorResponse(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    return new ErrorResponseModel400Example();
                case HttpStatusCode.NotFound:
                    return new ErrorResponseModel404Example();
                case HttpStatusCode.Unauthorized:
                    return new ErrorResponseModel500Example("breakdowns with the internal api key");
                default:
                    return new ErrorResponseModel();
            }
        }
    }
}
