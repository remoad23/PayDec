using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayDec.Shared.Model.Authentication
{
    public class CurrentUser
    {
        public bool IsAuthenticated { get; set; } = false;
        public List<Claim> Claims { get; set; } = new List<Claim>();
    }
}
