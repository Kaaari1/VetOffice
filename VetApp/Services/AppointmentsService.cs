using Microsoft.EntityFrameworkCore;
using VetApp.Controllers.Results;
using VetOffice.Models;

namespace VetApp.Services
{
    public class AppointmentsService
    {
        private DbSet<Visits> Appointemsts = new VetOfficeDbContext().Visit;

        public List<UserAppointmentsResult> GetAppointmentsByUserId(int userId)
        {
            var visits = Appointemsts.Where(x => x.id_user == userId).Include(x => x.Doctor).Include(x => x.Animal).ToList();
            var result = new List<UserAppointmentsResult>();

            foreach (var visit in visits)
            {
                result.Add(new UserAppointmentsResult()
                {
                    Date = visit.date,
                    Doctor = visit.User.name + " " + visit.User.surname,
                    Name = visit.Animal.name_a,
                    VisitId = visit.id_visit

                });
            }
            return result;
        }
    }
}
