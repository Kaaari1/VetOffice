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
                if (user.id_role == roleId)
                {
                    return;
                }
                else if(roleId != 3 && user.id_role == 3)
                {
                    var wasDoctor = db.Doctor.FirstOrDefault(x => x.id_user == userId);
                    wasDoctor.is_active = false;
                }
                else if (roleId == 3)
                {
                    var wasDoctor = db.Doctor.FirstOrDefault(x => x.id_user == userId);
                    if(wasDoctor == null)
                    {
                        var doctor = new Doctors()
                        {
                            id_user = userId,
                            work_hours_from = new TimeSpan(8, 0, 0),
                            work_hours_to = new TimeSpan(16, 0, 0),
                            is_active = true
                        };
                        db.Doctor.Add(doctor);
                    }
                }
                user.id_role = roleId;
                db.SaveChanges();
            }

        }
    }
}
