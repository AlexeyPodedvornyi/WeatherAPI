using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherAPI.Models.Responses.Interfaces;
using WeatherAPI.Serializer.Interfaces;

namespace WeatherAPI.Serializer
{
    public class ResponseModelSerializer : IResponseModelSerializer <IResponseModel>
    {
        public ContentResult ToJson(IResponseModel responseModel)
        {
            var json = JsonConvert.SerializeObject(responseModel);
            var response = new ContentResult
            {
                Content = json,
                ContentType = "application/json",
                StatusCode = responseModel.StatusCode
            };

            return response;
        }
    }
}
