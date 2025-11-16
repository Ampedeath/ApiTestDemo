using RestSharp;
using ApiTestDemo.Models;

namespace ApiTestDemo.Helper
{
    public class UserApiClient : ApiClient
    {
        private const string _endpoint = "/user";

        public ApiResponse CreateUser(User user)
        {
            var request = new RestRequest(_endpoint, Method.Post)
                .AddJsonBody(user);

            return Execute(request);
        }

        public ApiResponse GetUser(string username)
        {
            var request = new RestRequest($"{_endpoint}/{username}", Method.Get);

            return Execute(request);
        }

        public ApiResponse UpdateUser (string username, User user)
        {
            var request = new RestRequest($"{_endpoint}/{username}", Method.Put)
                .AddJsonBody(user);

            return Execute(request);
        }

        public ApiResponse DeleteUser(string username)
        {
            var request = new RestRequest($"{_endpoint}/{username}", Method.Delete);

            return Execute(request);
        }
    }
}
