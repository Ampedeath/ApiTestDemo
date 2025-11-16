using ApiTestDemo.Models;

namespace ApiTestDemo.Helper
{
    public class UserApiServise
    {
        private UserApiClient _client;

        public UserApiServise()
        {
            _client = new UserApiClient();
        }

        public void CreateUser(User user) 
        {
            _client.CreateUser(user);
        }

        public User GetUser(string username)
        {
            var response = _client.GetUser(username);

            return response.GetContent<User>();
        }

        public void UpdateUser(string username, User user)
        {
            _client.UpdateUser(username, user);
        }

        public void DeleteUser(string username)
        {
            _client.DeleteUser(username);
        }


        public ApiResponse TryGetUser(string username) => _client.GetUser(username);
        public ApiResponse TryUpdateUser(string username, User user) => _client.UpdateUser(username, user);
        public ApiResponse TryCreateUser(User user) => _client.CreateUser(user);
        public ApiResponse TryDeleteUser(string username) => _client.DeleteUser(username);
    }
}
