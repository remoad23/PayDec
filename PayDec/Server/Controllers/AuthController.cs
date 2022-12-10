using Microsoft.AspNetCore.Mvc;
using Nethereum.Contracts.Standards.ERC20.TokenList;
using PayDec.Server.Attributes;
using PayDec.Server.Model;
using PayDec.Server.Services;
using PayDec.Server.Services.Interfaces;
using PayDec.Shared.Model;
using PayDec.Shared.Model.Authentication;

namespace PayDec.Server.Controllers
{
    public class AuthController : Controller
    {

        private IJwtMaker JwtMaker { get; set; }
        private PayDecContext Context { get; set; }

        private IHttpContextAccessor ContextAccessor { get; set; }

        private IConfiguration Configuration { get; set; }  

        public AuthController(IJwtMaker jwtMaker,PayDecContext context,IConfiguration configuration)
        {
            Configuration = configuration;
            JwtMaker = jwtMaker;
            Context = context;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody]LoginRequest login)
        {
            if(login.Anonymous)
            {
                string token = JwtMaker.GenerateToken(anonymous: true);
                return Ok(token);
            }
            else
            {
                Admin? admin = Context.Admin.FirstOrDefault(x =>
                          x.Email.ToLower() == login.Email.ToLower()
                       && x.Password == login.Password
               );

                if (admin != null)
                {
                    string token = JwtMaker.GenerateToken(admin: admin);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }
            }
         
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }

        [Route("Authenticate")]
        [HttpGet]
        public IActionResult Authenticate()
        {
            bool isAuthenticated = false;
            string? token = HttpContext.Request.Headers.Authorization.ToString().Split(" ")[1];

            if (token == null) return Ok(isAuthenticated);
      
            JwtAuthorize auth = new JwtAuthorize(JwtMaker,Context,ContextAccessor,Configuration);
            isAuthenticated = auth.Authenticate(token, isAdmin: true);
            return Ok(isAuthenticated);
        }

        [Route("AuthenticateAnonymous")]
        [HttpGet]
        public IActionResult AuthenticateAnonymous()
        {
            bool isAuthenticated = false;
            string? token = HttpContext.Request.Headers.Authorization.ToString().Split(" ")[1];

            if (token == null) return Ok(isAuthenticated);

            JwtAuthorize auth = new JwtAuthorize(JwtMaker, Context, ContextAccessor, Configuration);
            isAuthenticated = auth.Authenticate(token);
            return Ok(isAuthenticated);
        }
    }
}
