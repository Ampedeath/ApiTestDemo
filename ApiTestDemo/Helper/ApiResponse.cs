using RestSharp;
using System.Text.Json;

namespace ApiTestDemo.Helper
{
    public class ApiResponse
    {
        public RestResponse Response { get; set; }

        public ApiResponse(RestResponse response) 
        {
            Response = response;
        }

        public int StatusCode => (int)Response.StatusCode;

        public T? GetContent<T>()
        {
            if (string.IsNullOrEmpty(Response.Content))
                return default;

            return JsonSerializer.Deserialize<T>(Response.Content);
        }

        public bool IsSuccess => Response.IsSuccessful;
    }
}
