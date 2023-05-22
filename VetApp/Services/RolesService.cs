using Microsoft.EntityFrameworkCore;
using VetApp.Controllers.Results;
using VetOffice.Models;

namespace VetApp.Services
{
    public class RolesService
    {
        private readonly DbSet<Roles> Roles = new VetOfficeDbContext().Role;
        private readonly VetOfficeDbContext db = new VetOfficeDbContext();

        public List<RolesResult> Get()
        {
            List<RolesResult> result = new List<RolesResult>();

            foreach (var role in Roles)
            {
                result.Add(new RolesResult()
                {
                    RoleName = role.role_name,
                    RoleId = role.id_role
                });
            }
            return result;
        }

        public void Update(int roleId, int userId)
        {
            var user = db.Users_Logins.FirstOrDefault(x => x.id_user == userId);
            if (user != null)
            {
                user.id_role = roleId;
                db.SaveChanges();
            }
        }
    }
}
