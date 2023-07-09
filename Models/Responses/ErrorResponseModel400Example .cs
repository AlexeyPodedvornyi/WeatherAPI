using Swagger.Net.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace WeatherAPI.Models.Responses
{
    public class ErrorResponseModel400Example : ErrorResponseModel ,IExamplesProvider<ErrorResponseModel400Example>
    {
        public ErrorResponseModel400Example()
        {
            StatusCode = 400;
            Type = "BadRequestError";
            Message = "Invalid request parameters";
        }
        public ErrorResponseModel400Example GetExamples()
        {
            return new ErrorResponseModel400Example();
        }
    }
}
