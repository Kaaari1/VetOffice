using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "Vet")]
    public class DoctorsController : ControllerBase
    {
        [HttpGet]
        [Route("doctor/dayoffs")]
        public List<DayOffResult> GetDayOffs()
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);
            List<DayOffResult> result = new List<DayOffResult>();

            if (userId > 0)
            {
                var doctorsService = new DoctorsService();
                result = doctorsService.GetDayOffs(userId);
            }
            return result;
        }

        [HttpPost]
        [Route("doctor/dayoff/remove/{dayoffId}")]
        public void RemoveDayOff(int dayoffId)
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);

            if (userId > 0)
            {
                var doctorsService = new DoctorsService();
                doctorsService.RemoveDayOff(dayoffId, userId);
            }
        }

        [HttpPost]
        [Route("doctor/dayoff/add/{date}")]
        public void AddDayOff(string date)
        {
            var dateTime = DateTime.Parse(date);

            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);

            if (userId > 0)
            {
                var doctorsService = new DoctorsService();
                doctorsService.AddDayOff(dateTime, userId);
            }
        }

        [HttpPost]
        [Route("updateWorkingHours/{from}/{to}")]
        public void UpdateWorkingHours(TimeSpan from, TimeSpan to)
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);

            if (userId > 0)
            {
                var doctorsService = new DoctorsService();
                doctorsService.UpdateWorkingHours(from, to, userId);
            }
        }

        [HttpGet]
        [Route("doctor/workingHours")]
        public WorkingHoursResult GetWorkingHours()
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);

            if (userId > 0)
            {
                var doctorsService = new DoctorsService();
                return doctorsService.GetWorkingHours(userId);
            }
            return new WorkingHoursResult();
        }
    }
}