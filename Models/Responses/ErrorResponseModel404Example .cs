using Swashbuckle.AspNetCore.Filters;

namespace WeatherAPI.Models.Responses
{
    public class ErrorResponseModel404Example : ErrorResponseModel, IExamplesProvider<ErrorResponseModel404Example>
    {
        public ErrorResponseModel404Example()
        {
            StatusCode = 404;
            Type = "NotFound";
            Message = "City not found";
        }
        public ErrorResponseModel404Example GetExamples()
        {
            return new ErrorResponseModel404Example();
        }
    
    }
}
