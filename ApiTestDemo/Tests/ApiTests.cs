using ApiTestDemo.Helper;
using ApiTestDemo.Models;
using FluentAssertions;

namespace ApiTestDemo.Tests
{
    public class ApiTests
    {
        private UserApiServise _service;
        private string _username;

        [SetUp]
        public void SetUp()
        {
            _service = new UserApiServise();
            _username = "qa_" + Guid.NewGuid().ToString("N").Substring(0, 6);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                _service.DeleteUser(_username);
            }
            catch
            {
                   
            }
        }

        // Positive tests

        [Test]
        public void CreateUserAndGetUserByUserName()
        {
            var user = BuildTestUser(_username);
            _service.CreateUser(user);

            var createUserResponse = _service.GetUser(user.username);

            createUserResponse.Should().NotBeNull();
            createUserResponse.username.Should().Be(user.username);
            createUserResponse.email.Should().Be(user.email);

        }

        [Test]
        public void GetUser_ShouldReturnExistingUser()
        {
            var user = BuildTestUser(_username);
            _service.CreateUser(user);

            var getUserResponse = _service.GetUser(user.username);

            getUserResponse.username.Should().Be(user.username);
            getUserResponse.email.Should().Be(user.email);
            getUserResponse.firstName.Should().Be("Rayan");
        }

        [Test]
        public void UpdateUser_ShouldChangeLastNameAndEmail()
        {
            var user = BuildTestUser(_username);
            _service.CreateUser(user);

            user.lastName = "Gosling";
            user.email = "new_" + user.email;


            _service.UpdateUser(user.username, user);

            var getUpdatedUser = _service.GetUser(user.username);

            getUpdatedUser.Should().NotBeNull();
            getUpdatedUser.lastName.Should().Be("Gosling");
            getUpdatedUser.email.Should().Be($"{user.username}_new@mail.com");
        }

        [Test]
        public void DeleteUser_ShouldRemoveUser()
        {
            var user = BuildTestUser(_username);
            _service.CreateUser(user);

            _service.DeleteUser(user.username);

            var response = _service.TryGetUser(user.username);

            response.StatusCode.Should().Be(404);
        }

        // Hegative tests

        [Test]
        public void GetUserShouldReturn404UserDoesNotExist()
        {
            var GetUnregisteredUser = _service.TryGetUser("UnExisting");
            GetUnregisteredUser.Should().NotBeNull();
            GetUnregisteredUser.StatusCode.Should().Be(404);
        }

        [Test] 
        public void DeleteUserShouldReturn404UserDoesNotExist()
        {
            var DeleteUnregisteredUser = _service.TryDeleteUser("UnExisting");
            DeleteUnregisteredUser.Should().NotBeNull();
            DeleteUnregisteredUser.StatusCode.Should().Be(404);
        }

        [Test]
        public void CreateUserShouldReturnErrorBodyIsInvalid()
        {
            var invalisRequestBody = new User();

            var CreateUnregisteredUser = _service.TryCreateUser(invalisRequestBody);

            CreateUnregisteredUser.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void UpdateUserShouldReturnUserDoesNotExist()
        {
            var InvalidUpdateRequest = BuildTestUser("ghost_123");
            var UpdateUnregisteredUser = _service.TryUpdateUser(InvalidUpdateRequest.username, InvalidUpdateRequest);

            UpdateUnregisteredUser.IsSuccess.Should().BeTrue();
        }

        private User BuildTestUser(string username)
        {
            return new User
            {
                id = 0,
                username = username,
                firstName = "Rayan",
                lastName = "Tester",
                email = username + "@mail.com",
                password = "123456",
                phone = "111111",
                userStatus = 1
            };
        }

    }
}
