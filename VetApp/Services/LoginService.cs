using Microsoft.EntityFrameworkCore;
using VetOffice.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace VetApi.Services
{
    public class LoginService
    {
        private DbSet<Users_login> UsersLogin = new VetOfficeDbContext().Users_Logins;

        public string Login(string email, string password)
        {
            var user = UsersLogin.Include(x => x.User).Include(x => x.Role).FirstOrDefault(x => x.email == email && x.password == password);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim("name", user.User.name),
                    new Claim("role", user.Role.role_name),
                };

                byte[] keyBytes = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(keyBytes);
                }

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-bardzo-sekretny-klucz-dla-tokenu"));

                var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenString;

            }
            return string.Empty;
        }
    }
}
