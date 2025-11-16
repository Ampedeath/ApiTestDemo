using RestSharp;

namespace ApiTestDemo.Helper
{
    public class ApiClient 
    {
        private readonly RestClient _client;

        private string BaseUrl = "https://petstore.swagger.io/v2";

        public ApiClient()
        {
            _client = new RestClient(BaseUrl);
        }

        public ApiResponse Execute(RestRequest request)
        {
            var response = _client.Execute(request);
            return new ApiResponse(response);
        }
    }
}
