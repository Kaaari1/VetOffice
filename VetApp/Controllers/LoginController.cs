using Microsoft.AspNetCore.Mvc;
using VetApi.Services;
using VetApp.Controllers.Results;

namespace VetApi.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [Route("login/{login}/{password}")]
        public LoginResult Login(string login, string password)
        {
            var loginService = new LoginService();
            var result = loginService.Login(login, password);
            return result;
        }
    }
}