using Microsoft.AspNetCore.Mvc;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    public class PetsController : ControllerBase
    {
        [HttpGet]
        [Route("animals")]
        public List<GetPetsResult> Get()
        {
            var getPetsService = new GetPetsService();
            var result = getPetsService.Get();
            return result;
        }
    }

}
