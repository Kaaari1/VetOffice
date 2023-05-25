using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using VetApp.Controllers.Results;
using VetApp.Services;
using VetOffice.Models;

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
        [HttpGet]
        [Route("appointments/{visitId}")]
        public GetAppointmentResult GetAppointment(int visitId)
        {
            var appointmentsService = new AppointmentsService();
            var result = appointmentsService.GetAppointment(visitId);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("appointments/{visitId}/{userId}/{dateTime}")]
        public void UpdateVisitDateTime(DateTime dateTime, int visitId, int userId)
        {
            var appointmentsService = new AppointmentsService();
            appointmentsService.UpdateVisitDateTime(dateTime, visitId, userId);
        }


        [HttpPost]
       
        [Route("appointments/{animalId}/{doctorId}/{date}/{time}")]
        public void AddAppointment(int animalId, int doctorId, DateTime date, string time)
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);

            if (userId > 0)
            {
                var appointmentsService = new AppointmentsService();
                appointmentsService.AddAppointment(userId, animalId, doctorId, date, time);

            }
            
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("appointments/{visitId}")]
        public void RemoveAppointment(int visitId)
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);

            if (userId > 0)
            {
                var appointmentsService = new AppointmentsService();
                appointmentsService.RemoveAppointment(visitId, userId);

            }
            
        }
        
    }
}