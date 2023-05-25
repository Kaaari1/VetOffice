using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VetApp.Controllers.Results;
using VetOffice.Models;

namespace VetApi.Services
{
    public class LoginService
    {
        private DbSet<Users_login> UsersLogin = new VetOfficeDbContext().Users_Logins;
        private readonly VetOfficeDbContext dbContext = new VetOfficeDbContext();

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
                    new Claim("userId", user.User.id_user.ToString()),
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
                    UserId = user.id_user
                };
                return result;
            }
            return new LoginResult(); ;
        }



        public LoginResult Register(string email, string password, string surname, string phoneNumber, string name)
        {
            var reg = UsersLogin.Include(x => x.User).Include(x => x.Role).FirstOrDefault(x => x.email == email);

            if (reg == null)
            {
                var user = new Users() { name = name, surname = surname, phone_number = phoneNumber };
                dbContext.Add(user);
                dbContext.SaveChanges();
                var userlogin = new Users_login() { email = email, password = password, id_user = user.id_user, id_role = 2 };
                dbContext.SaveChanges();
            }
            return Login(email, password);
        }
    }
}
