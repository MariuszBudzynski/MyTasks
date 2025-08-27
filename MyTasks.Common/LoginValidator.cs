using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyTasks.Common.Interfaces;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.ILoginRepository;
using MyTasks.Repositories.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyTasks.Common
{
    public class LoginValidator : ILoginValidator
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginValidator(
            ILoginRepository loginRepository,
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _loginRepository = loginRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> ValidateLogin(LoginRequest request)
        {
            try
            {
                var loginUser = await _loginRepository.GetUserLoginDataByUserName(request.Username);

                if (loginUser == null)
                {
                    return new JsonResult(new { success = false, message = "Login data for user not found" }) { StatusCode = 404 };
                }

                var user = await _loginRepository.GetUserDataById(loginUser.UserId);

                if (user == null )
                {
                    return new JsonResult(new { success = false, message = "User not found" }) { StatusCode = 404 };
                }

                var passwordValidation = loginUser.FakeUser
                ? request.Password == loginUser.PasswordHash  // plain text check
                : PasswordHasher.VerifyPassword(loginUser.PasswordHash, request.Password); // hashed check

                if (request.Username == loginUser.Username && passwordValidation && !user.IsDeleted)
                {
                    var token = GenerateJwtToken(loginUser);
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset
                                 .UtcNow
                                 .AddMinutes(_config.GetValue<int>("Jwt:ExpireMinutes"))
                    };

                    var response = _httpContextAccessor.HttpContext?.Response;
                    if (response == null)
                    {
                        return new JsonResult(new { success = false, message = "Unable to access HTTP response." }) { StatusCode = 500 };
                    }

                    response.Cookies.Append("AuthToken", token, cookieOptions);

                    return new JsonResult(new { success = true });
                }

                return new JsonResult(new { success = false, message = "Invalid login" })
                {
                    StatusCode = 401
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Internal server error {ex.Message}." }) { StatusCode = 500 };
            }
        }

        private string GenerateJwtToken(LoginModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", user.Type.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpireMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
