using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApp.Controllers.Results;
using VetApp.Services;

namespace VetApi.Controllers
{
    [ApiController]
    public class RolesController : ControllerBase
    {
        [HttpGet]
        [Route("roles")]
        [Authorize(Roles = "Admin")]
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