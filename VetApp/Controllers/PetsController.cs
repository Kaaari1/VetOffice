using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "User,Vet")]
        public List<GetPetsResult> Get()
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);
            List<GetPetsResult> result = new List<GetPetsResult>();
            if (userId > 0)
            {
                var getPetsService = new GetPetsService();
                result = getPetsService.Get(userId);
            }
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("animal/add/{name}/{type}/{birthday}")]
        public void AddPet(string name, string type, DateTime birthday)
        {
            var authService = new AuthService();
            int userId = authService.GetUserIdFromToken(HttpContext);
            if (userId > 0)
            {
                var getPetsService = new GetPetsService();
                getPetsService.AddPet(userId, name, type, birthday);
            }
        }
    }

}
