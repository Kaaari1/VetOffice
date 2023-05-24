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
            var doctors = dbContext.Doctor.Include(x => x.User).ToList();

            foreach (var doctor in doctors)
            {
                result.Add(new DoctorsResult()
                {
                    Name = doctor.User.name,
                    DoctorId = doctor.id_doctor,
                });
            }
            return result;
        }
    }
}
