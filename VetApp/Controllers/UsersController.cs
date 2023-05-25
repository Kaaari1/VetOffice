using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "Admin")]
        public List<UsersResult> Get()
        {
            var usersService = new UsersService();
            var result = usersService.Get();
            return result;
        }

        [HttpGet]
        [Route("doctors")]
        public List<DoctorsResult> GetDoctors()
        {
            var doctorsService = new DoctorsService();
            var result = doctorsService.Get();
            return result;
        }

        [HttpGet]
        [Route("doctor/hours/{doctorId}/{date}")]
        public List<string> GetDoctorHours(int doctorId, DateTime date)
        {
            var doctorsService = new DoctorsService();
            var result = doctorsService.GetDoctorHours(doctorId, date);
            return result;
        }
    }
}