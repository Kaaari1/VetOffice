using Microsoft.EntityFrameworkCore;
using VetApp.Controllers.Results;
using VetOffice.Models;

namespace VetApp.Services
{
    public class AppointmentsService
    {
        private DbSet<Visits> Appointemsts = new VetOfficeDbContext().Visit;
        private VetOfficeDbContext DbContext = new VetOfficeDbContext();

        public List<UserAppointmentsResult> GetAppointmentsByUserId(int userId)
        {
            var visits = Appointemsts.Where(x => x.id_user == userId && x.is_active).Include(x => x.Doctor).Include(x => x.Animal).Include(x => x.User).ToList();
            var result = new List<UserAppointmentsResult>();

            foreach (var visit in visits)
            {
                var doc = DbContext.Doctor.Include(x => x.User).FirstOrDefault(x => x.id_doctor == visit.id_doctor);

                result.Add(new UserAppointmentsResult()
                {
                    Date = visit.date,
                    Doctor = $"{doc.User.name} {doc.User.surname}",
                    AnimalName = visit.Animal.name_a,
                    VisitId = visit.id_visit

                });
            }
            return result;
        }
        public List<VetAppointmentsResult> GetAppointmentsByVetId(int userId)
        {
            int vetId = DbContext.Doctor.FirstOrDefault(x => x.id_user == userId).id_doctor;
            var visits = Appointemsts.Where(x => x.id_doctor == vetId && x.is_active).Include(x => x.Doctor).Include(x => x.Animal).Include(x => x.User).ToList();
            var result = new List<VetAppointmentsResult>();

            foreach (var visit in visits)
            {
                result.Add(new VetAppointmentsResult()
                {
                    Date = visit.date,
                    Name = visit.User.name + " " + visit.User.surname,
                    AnimalName = visit.Animal.name_a,
                    VisitId = visit.id_visit

                });
            }
            return result;
        }

        public void UpdateVisitDateTime(DateTime dateTime, int visitId, int userId)
        {
            var visit = DbContext.Visit.Include(x => x.Doctor)
                .FirstOrDefault(x => x.id_visit == visitId && x.is_active && (x.Doctor.id_user == userId || x.id_user == userId));
            if (visit != null)
            {
                visit.date = dateTime;
                DbContext.SaveChanges();
            }
        }

        public bool HasAccess(int userId, int visitId)
        {
            var asd = DbContext.Visit.Include(x => x.Doctor)
                .Select(x => x.id_visit == visitId && x.is_active && (x.Doctor.id_user == userId || x.id_user == userId)).FirstOrDefault();
            return asd;
        }
    }
}
