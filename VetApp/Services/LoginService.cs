using Microsoft.EntityFrameworkCore;
using VetOffice.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using VetApp.Controllers.Results;

namespace VetApi.Services
{
    public class LoginService
    {
        private DbSet<Users_login> UsersLogin = new VetOfficeDbContext().Users_Logins;

        public LoginResult Login(string email, string password)
        {
            var user = UsersLogin.Include(x => x.User).Include(x => x.Role).FirstOrDefault(x => x.email == email && x.password == password);

            if (user != null)
            {
                var secretKey = "super-bardzo-sekretny-klucz-dla-tokenu";
                var issuer = "your-issuer";
                var audience = "your-audience";

                var claims = new[]
                {
                    new Claim("name", user.User.name),
                    new Claim("roles", user.Role.role_name),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.UtcNow.AddHours(1); // Set the expiration time as desired

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: expires,
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                var result = new LoginResult()
                {
                    Token = tokenString,
                    Role = user.Role.role_name,
                    UserId = user.id_user.ToString()
                };

                return result;

            }
            return new LoginResult(); ;
        }
    }
}
