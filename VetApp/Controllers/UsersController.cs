using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "Admin")]
        public List<UsersResult> Get()
        {
            var usersService = new UsersService();
            var result = usersService.Get();
            return result;
        }
    }
}