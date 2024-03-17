using System.Threading.Tasks;
using event_driven_backend.Controllers;
using event_driven_backend.DTOs;
using event_driven_backend.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace event_driven_backend.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _controller;
        private DbContextOptions<Context> _options;

        [SetUp]
        public void SetUp()
        {
            using (var context = new Context(_options))
            {
                context.Users.Add(new User
                {
                    Name = "Test",
                    Surname = "User",
                    Email = "test@example.com",
                    Password = Argon2.Hash("password")
                });
                context.SaveChanges();
            }

            var con = new Context(_options);
            _controller = new UserController(null, con);

        }

        [Test]
        public async Task Login_ValidCredentials_ReturnOkResultWithUserData()
        {
            var loginDto = new LoginDTO
            {
                Email = "test@example.com",
                Password = "password"
            };

            var result = await _controller.Login(loginDto) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(300));

            var user = result.Value as User;

            Assert.That(user, Is.Not.Null);
            Assert.That(user.Name, Is.EqualTo("Test"));
            Assert.That(user.Surname, Is.EqualTo("User"));
            Assert.That(user.Email, Is.EqualTo("test@example.com"));





        }
    }
}
