using Microsoft.AspNetCore.Mvc;
using VetApi.Services;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
   

        [ApiController]
        public class GetPetsController : ControllerBase
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
