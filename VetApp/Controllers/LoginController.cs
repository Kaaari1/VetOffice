using Microsoft.AspNetCore.Mvc;
using VetApi.Services;

namespace VetApi.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [Route("login/{login}/{haslo}")]
        public string Login(string login, string haslo)
        {
            var loginService = new LoginService();

            return loginService.Login(login, haslo);
        }
    }
}