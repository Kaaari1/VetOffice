using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApi.Services;
using VetApp.Controllers.Results;

namespace VetApi.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [Route("login/{login}/{haslo}")]
        public LoginResult Login(string login, string haslo)
        {
            var loginService = new LoginService();
            var result = loginService.Login(login, haslo);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("test")]
        public bool xd()
        {
            return true;
        }
    }
}