using Microsoft.AspNetCore.Mvc;
using PayDec.Server.Services.Interfaces;
using PayDec.Shared.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PayDec.Server.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using PayDec.Server.Services;
using NBitcoin.Secp256k1;

namespace PayDec.Server.Attributes
{
    public class JwtAuthorize : IAuthorizationFilter
    {

        private IJwtMaker Maker { get; set; }
        private PayDecContext Context { get; set; }
        private IHttpContextAccessor ContextAccessor { get; set; }

        private IConfiguration Configuration { get; set; }  

        public JwtAuthorize(IJwtMaker maker, PayDecContext context, IHttpContextAccessor contextAccessor,IConfiguration configuration)
        {
            Configuration = configuration;
            Maker = maker;
            Context = context;
            ContextAccessor = contextAccessor;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = ContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split("Bearer ")[1];
            
            JwtSecurityToken decryptedToken = (Maker as JwtMaker).DecryptToken(token);


            List<Claim> claims = decryptedToken.Claims.ToList();

            long expirationDate = Int64.Parse(claims.FirstOrDefault(c => c.Type == "exp").Value);
            long nowDate = DateTimeOffset.Now.ToUnixTimeSeconds();
            string audience = claims.FirstOrDefault(c => c.Type == "aud").Value;

            if (nowDate > expirationDate || audience != Configuration["Jwt:Audience"])
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                // check anonymous
                if(claims.FirstOrDefault(c => c.Type == ClaimTypes.Anonymous)?.Value == "Anonymous")
                {

                }
                // check actual login auth
                else
                {
                    string email = claims.First(c => c.Type == ClaimTypes.Email).Value;
                    string userName = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    Admin? admin = Context.Admin.FirstOrDefault(x =>
                                   x.Email.ToLower() == email
                                && x.Name.ToLower() == userName
                        );

                    if (admin == null)
                        context.Result = new UnauthorizedResult();
                }
               
            }
        }

        public bool Authenticate(string token,bool isAdmin = false)
        {

            JwtSecurityToken decryptedToken = (Maker as JwtMaker).DecryptToken(token);


            List<Claim> claims = decryptedToken.Claims.ToList();

            long expirationDate = Int64.Parse(claims.FirstOrDefault(c => c.Type == "exp").Value);
            long nowDate = DateTimeOffset.Now.ToUnixTimeSeconds();
            string audience= claims.FirstOrDefault(c => c.Type == "aud").Value;

            if ( nowDate > expirationDate || audience != Configuration["Jwt:Audience"])
            {
                return false;
            }

            // check anonymous
            if (claims.FirstOrDefault(c => c.Type == ClaimTypes.Anonymous)?.Value == "Anonymous" && !isAdmin )
            {
                return true;
            }
            // check actual login auth
            else
            {
                if(isAdmin)
                {
                    string? email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                    string? userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                    if (email == null || userName == null)
                        return false;

                    Admin? admin = Context.Admin.FirstOrDefault(x =>
                                   x.Email.ToLower() == email
                                && x.Name.ToLower() == userName
                        );

                    return admin != null;
                }
                return false;
            }
        }
    }
}
