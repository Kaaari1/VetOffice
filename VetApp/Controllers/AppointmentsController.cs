using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    public class AppointmentsController : AuthController
    {
        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("appointments/{userId}")]
        public List<UserAppointmentsResult> GetAppointmentsByUserId(int userId)
        {
            var appointmentsService = new AppointmentsService();
            var result = appointmentsService.GetAppointmentsByUserId(userId);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("has-access/{visitId}")]
        public bool HasAccess(int visitId)
        {
            int userId = GetUserIdFromToken();

            if (userId > 0)
            {
                var appointmentsService = new AppointmentsService();
                return appointmentsService.HasAccess(userId, visitId);

            }
            return false;

        }


        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("appointments/{visitId}/{userId}/{dateTime}")]
        public void UpdateVisitDateTime(DateTime dateTime, int visitId, int userId)
        {
            var appointmentsService = new AppointmentsService();
            appointmentsService.UpdateVisitDateTime(dateTime, visitId, userId);
        }
    }
}