using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using MyTasks.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyTasks.Tests
{
    public class JwtHelperTests
    {
        private readonly IHttpContextAccessor _fakeHttpContextAccessor;
        private readonly JwtHelper _sut;

        public JwtHelperTests()
        {
            _fakeHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            _sut = new JwtHelper(_fakeHttpContextAccessor);
        }


        [Fact]
        public void GetLoggedInUserName_ShouldReturnNull_WhenNoHttpContext()
        {
            // Arrange
            A.CallTo(() => _fakeHttpContextAccessor.HttpContext).Returns(null);

            // Act
            var result = _sut.GetLoggedInUserName();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetLoggedInUserName_ShouldReturnNull_WhenNoAuthTokenCookie()
        {
            // Arrange
            var context = new DefaultHttpContext();
            A.CallTo(() => _fakeHttpContextAccessor.HttpContext).Returns(context);

            // Act
            var result = _sut.GetLoggedInUserName();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetLoggedInUserName_ShouldReturnUserName_WhenValidJwtExists()
        {
            // Arrange
            var username = "testuser";
            var token = GenerateJwtToken(username);

            var context = new DefaultHttpContext();
            context.Request.Cookies = new RequestCookieCollection(
                new Dictionary<string, string> { { "AuthToken", token } }
            );

            A.CallTo(() => _fakeHttpContextAccessor.HttpContext).Returns(context);

            // Act
            var result = _sut.GetLoggedInUserName();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result);
        }

        private string GenerateJwtToken(string subject)
        {
            var handler = new JwtSecurityTokenHandler();

            var token = new JwtSecurityToken(
                claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, subject) },
                expires: DateTime.UtcNow.AddHours(1)
            );

            return handler.WriteToken(token);
        }
    }
}
