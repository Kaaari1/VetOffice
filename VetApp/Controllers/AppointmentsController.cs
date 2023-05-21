using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("appointments/{userId}")]
        public List<UserAppointmentsResult> GetAppointmentsByUserId(string userId)
        {
            int.TryParse(userId, out int id);
            var loginService = new AppointmentsService();
            var result = loginService.GetAppointmentsByUserId(id);
            return result;
        }
    }
}