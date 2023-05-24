using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        [HttpGet]
        [Route("doctors")]
        public List<DoctorsResult> Get()
        {
            var doctorsService = new DoctorsService();
            var result = doctorsService.Get();
            return result;
        }
    }
}