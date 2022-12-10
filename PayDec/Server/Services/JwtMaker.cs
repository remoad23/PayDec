using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using PayDec.Server.Model;
using PayDec.Shared.Model;
using PayDec.Shared.Model.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using PayDec.Server.Services.Interfaces;
using System.ComponentModel;

namespace PayDec.Server.Services
{
    public class JwtMaker : IJwtMaker
    {

        private readonly IConfiguration Config;
        public JwtMaker(IConfiguration config)
        {
            Config = config;
        }


        // To generate token
        public string GenerateToken(Admin? admin = null,bool anonymous = false)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = !anonymous ? new List<Claim>
            {
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.NameIdentifier, admin.Name)
            } :
            new List<Claim>
            {
                    new Claim(ClaimTypes.Anonymous, "Anonymous")
            };


            var token = new JwtSecurityToken(Config["Jwt:Issuer"],
                Config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            string rawToken = new JwtSecurityTokenHandler().WriteToken(token);

            return rawToken;

        }

        public JwtSecurityToken DecryptToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken encryptedJwtToken = tokenHandler.ReadJwtToken(token);

            return encryptedJwtToken;
        }


        public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);

                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}
