using MyTasks.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace MyTasks.Services
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public JwtHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetLoggedInUserName()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            return jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        }
    }
}
