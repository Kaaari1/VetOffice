using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        [HttpGet]
        [Route("roles")]
        public List<RolesResult> Get()
        {
            var rolesService = new RolesService();
            var result = rolesService.Get();
            return result;
        }
        [HttpPost]
        [Route("roles/{roleId}/{userId}")]
        public void Update(int roleId, int userId)
        {
            var rolesService = new RolesService();
            rolesService.Update(roleId, userId);
        }
    }
}