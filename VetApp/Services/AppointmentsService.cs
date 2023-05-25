using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VetApp.Controllers.Results;
using VetApp.Migrations;
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

        public void UpdateVisitDateTime(DateTime date, int visitId, int doctorId, string time, int userId)
        {
            var visit = DbContext.Visit.Include(x => x.Doctor)
                .FirstOrDefault(x => x.id_visit == visitId && x.is_active && (x.Doctor.id_user == userId || x.id_user == userId));
            if (visit != null)
            {
                var timespan = TimeSpan.Parse(time);
                visit.date = date.Date + timespan;
                DbContext.SaveChanges();
            }
        }

        public bool HasAccess(int userId, int visitId)
        {
            var visit = DbContext.Visit.Include(x => x.Doctor).FirstOrDefault(x => x.id_visit == visitId);
            if (visit != null)
            {
                return visit.id_user == userId || visit.Doctor.id_user == userId;

            }
            return false;
        }

        public void AddAppointment(int userId, int animalId, int doctorId, DateTime date, string time)
        {
            var timespan = TimeSpan.Parse(time);
            var visit = new Visits()
            {
                id_animal = animalId,
                id_doctor = doctorId,
                id_user = userId,
                date = date.Date + timespan,
                is_active = true
            };
            DbContext.Visit.Add(visit);
            DbContext.SaveChanges();
        }

        public void RemoveAppointment(int visitId, int userId)
        {
            var visit = DbContext.Visit.Include(x => x.Doctor)
                .FirstOrDefault(x => x.id_visit == visitId && x.is_active && (x.Doctor.id_user == userId || x.id_user == userId));
            if (visit != null)
            {
                visit.is_active = false;
                DbContext.SaveChanges();
            };
        }

        public GetAppointmentResult GetAppointment(int visitId)
        {
            var visits = DbContext.Visit.Include(x => x.Doctor).Include(x => x.Animal).Include(x => x.User).FirstOrDefault(x => x.id_visit == visitId);
            if (visits == null) return new GetAppointmentResult();
            var pets = DbContext.Animal.Where(x => x.id_user == visits.id_user).ToList();
            var doctors = DbContext.Doctor.Include(x => x.User).Where(x => x.is_active).ToList();

            List<Pet> petsResult = new List<Pet>();
            List<Doctor> doctorsResult = new List<Doctor>();

            foreach (var pet in pets)
            {
                petsResult.Add(new Pet()
                {
                    PetId = pet.id_animal,
                    PetName = pet.name_a
                });
            }

            foreach (var doctor in doctors)
            {
                doctorsResult.Add(new Doctor()
                {
                    DoctorId = doctor.id_doctor,
                    DoctorName = $"{doctor.User.name} {doctor.User.surname}"
                });
            }

            return new GetAppointmentResult()
            {
                PetId = visits.id_animal,
                DoctorId = visits.id_doctor,
                DateTime = visits.date.TimeOfDay.ToString(),
                Date = visits.date,
                Pets = petsResult,
                Doctors = doctorsResult
            };

        }
    }
}
