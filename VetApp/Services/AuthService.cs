using System.IdentityModel.Tokens.Jwt;

namespace VetApp.Services
{
    public class AuthService
    {
        public int GetUserIdFromToken(HttpContext httpContext)
        {
            string authorizationHeader = httpContext.Request.Headers["Authorization"];

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
