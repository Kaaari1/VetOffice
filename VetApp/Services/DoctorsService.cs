using Microsoft.EntityFrameworkCore;
using VetApp.Controllers.Results;
using VetOffice.Models;

namespace VetApp.Services
{
    public class DoctorsService
    {
        private readonly VetOfficeDbContext dbContext = new VetOfficeDbContext();

        public List<DoctorsResult> Get()
        {
            List<DoctorsResult> result = new List<DoctorsResult>();
            var doctors = dbContext.Doctor.Where(x => x.is_active).Include(x => x.User).ToList();

            foreach (var doctor in doctors)
            {
                result.Add(new DoctorsResult()
                {
                    Name = $"{doctor.User.name} {doctor.User.surname}",
                    DoctorId = doctor.id_doctor,
                });
            }
            return result;
        }

        public List<string> GetDoctorHours(int doctorId, DateTime date)
        {
            List<string> result = new();
            var doctor = dbContext.Doctor.FirstOrDefault(x => x.id_doctor == doctorId && x.is_active);
            if (doctor != null)
            {
                var presence = dbContext.Presen.FirstOrDefault(x => x.id_doctor == doctorId && x.nworking_days.Date == date.Date && x.is_active);

                if (presence == null)
                {
                    List<string> visitsList = new();
                    var visitsThisDay = dbContext.Visit.Where(x => x.id_doctor == doctor.id_doctor && x.is_active && x.date.Date == date.Date).ToList();
                    foreach (var visit in visitsThisDay)
                    {
                        visitsList.Add(visit.date.TimeOfDay.ToString());
                    }

                    var generatedHours = GenerateVisitHours(doctor.work_hours_from, doctor.work_hours_to);

                    result = generatedHours.Except(visitsList).ToList();
                }
            }
            return result;
        }


        public List<DayOffResult> GetDayOffs(int userId)
        {
            List<DayOffResult> result = new List<DayOffResult>();
            var doctor = dbContext.Doctor.FirstOrDefault(x => x.id_user == userId && x.is_active);
            var dayoffs = dbContext.Presen.Where(x => x.id_doctor == doctor.id_doctor && x.is_active).ToList();

            foreach (var dayoff in dayoffs)
            {
                result.Add(new DayOffResult()
                {
                    Date = dayoff.nworking_days,
                    DayOffId = dayoff.dayoff_id,
                });
            }
            return result;
        }

        public void RemoveDayOff(int dayoffId, int userId)
        {
            var doctor = dbContext.Doctor.FirstOrDefault(x => x.id_user == userId && x.is_active);
            if (doctor != null)
            {
                var dayoff = dbContext.Presen.FirstOrDefault(x => x.id_doctor == doctor.id_doctor && x.dayoff_id == dayoffId);
                if (dayoff != null)
                {
                    dayoff.is_active = false;
                    dbContext.SaveChanges();
                }
            }
        }
        public void AddDayOff(DateTime date, int userId)
        {
            var doctor = dbContext.Doctor.FirstOrDefault(x => x.id_user == userId && x.is_active);
            if (doctor != null)
            {
                var dayoffs = dbContext.Presen.Where(x => x.id_doctor == doctor.id_doctor && x.is_active).ToList();
                if (!dayoffs.Exists(obj => obj.nworking_days == date))
                {
                    dbContext.Presen.Add(new Presence()
                    {
                        id_doctor = doctor.id_doctor,
                        nworking_days = date,
                        is_active = true,
                    });
                    dbContext.SaveChanges();
                }
            }

        }

        public void UpdateWorkingHours(TimeSpan from, TimeSpan to, int userId)
        {
            var doctor = dbContext.Doctor.FirstOrDefault(x => x.id_user == userId && x.is_active);
            if (doctor != null)
            {
                doctor.work_hours_to = to;
                doctor.work_hours_from = from;
                dbContext.SaveChanges();
            }

        }

        public WorkingHoursResult GetWorkingHours(int userId)
        {
            var doctor = dbContext.Doctor.FirstOrDefault(x => x.id_user == userId && x.is_active);
            if (doctor != null)
            {
                return new WorkingHoursResult()
                {
                    from = doctor.work_hours_from,
                    to = doctor.work_hours_to,
                };
            }
            return new WorkingHoursResult();

        }

        private static List<string> GenerateVisitHours(TimeSpan startTime, TimeSpan endTime)
        {
            List<string> timeSpans = new List<string>();

            TimeSpan currentTime = startTime;

            while (currentTime <= endTime)
            {
                timeSpans.Add(currentTime.ToString());
                currentTime = currentTime.Add(new TimeSpan(1, 0, 0));
            }

            return timeSpans;
        }
    }
}
