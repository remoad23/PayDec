using Microsoft.AspNetCore.Mvc;
using PayDec.Server.Attributes;
using PayDec.Server.Model;
using PayDec.Shared.Model;

namespace PayDec.Server.Controllers
{
    public class AdminController : Controller
    {
        private PayDecContext Context { get; set; }

        public AdminController(PayDecContext context)
        {
            this.Context = context;
        }

        [Route("Internal/Admin")]
        [TypeFilter(typeof(JwtAuthorize))]
        public IActionResult Get(int id)
        {
            return Ok(Context.Item.First(p => p.Id == id));
        }

    }
}
