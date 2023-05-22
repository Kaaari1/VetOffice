using Microsoft.EntityFrameworkCore;
using VetApp.Controllers.Results;
using VetOffice.Models;

namespace VetApp.Services
{
    public class UsersService
    {
        private readonly DbSet<Users_login> Users = new VetOfficeDbContext().Users_Logins;

        public List<UsersResult> Get()
        {
            List<UsersResult> result = new List<UsersResult>();
            var users = Users.Include(x => x.User).Include(x => x.Role).ToList();

            foreach (var user in users)
            {
                result.Add(new UsersResult()
                {
                    Email = user.email,
                    RoleId = user.id_role,
                    Name = user.User.name,
                    Surname = user.User.surname,
                    RoleName = user.Role.role_name,
                    UserId = user.id_user,
                });
            }
            return result;
        }
    }
}
