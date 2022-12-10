using Microsoft.IdentityModel.Tokens;
using NBitcoin.Secp256k1;
using PayDec.Shared.Model.Authentication;
using PayDec.Shared.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PayDec.Server.Services.Interfaces
{
    public interface IJwtMaker
    {
        public string GenerateToken(Admin? admin = null, bool anonymous = false);

        public JwtSecurityToken DecryptToken(string token);

        public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding);

        public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding);
    }
}
