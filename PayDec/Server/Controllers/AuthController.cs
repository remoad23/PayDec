using Microsoft.AspNetCore.Mvc;

namespace PayDec.Server.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            return View();
        }
    }
}
