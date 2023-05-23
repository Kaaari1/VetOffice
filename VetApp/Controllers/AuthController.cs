using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace VetApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Route("auth")]
        public int GetUserIdFromToken()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                string? customValue = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                if (customValue != null)
                {
                    int.TryParse(customValue, out var userId);
                    if (userId > 0)
                    {
                        return userId;
                    }
                }
            }
            return 0;
        }
    }
}