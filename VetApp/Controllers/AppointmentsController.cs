using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("appointments")]
        public List<UserAppointmentsResult> GetAppointmentsByUserId()
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);
            List<UserAppointmentsResult> result = new List<UserAppointmentsResult>();
            if (userId > 0)
            {
                var appointmentsService = new AppointmentsService();
                result = appointmentsService.GetAppointmentsByUserId(userId);
            }
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Vet")]
        [Route("appointments/vet")]
        public List<VetAppointmentsResult> GetAppointmentsByVetId()
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);
            List<VetAppointmentsResult> result = new List<VetAppointmentsResult>();
            if (userId > 0)
            {
                var appointmentsService = new AppointmentsService();
                result = appointmentsService.GetAppointmentsByVetId(userId);
            }

            return result;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("has-access/{visitId}")]
        public bool HasAccess(int visitId)
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);

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