using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                string? customValue = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                if (customValue != null)
                {
                    int.TryParse(customValue, out var userId);
                    if (userId > 0)
                    {
                        var appointmentsService = new AppointmentsService();
                        return appointmentsService.HasAccess(userId, visitId);

                    }
                }
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