using Microsoft.AspNetCore.Mvc;

namespace PayDec.Server.Controllers
{
    public class AuthController : Controller
    {

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            return View();
        }

        [Route("Authenticate")]
        public IActionResult Authenticate()
        {
            return View();
        }
    }
}
