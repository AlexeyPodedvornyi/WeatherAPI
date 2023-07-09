using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models.Responses.Interfaces;

namespace WeatherAPI.Serializer.Interfaces
{
    public interface IResponseModelSerializer<in TResponseModel> where TResponseModel : IResponseModel
    {
        ContentResult ToJson(TResponseModel responseModel);
    }
}
