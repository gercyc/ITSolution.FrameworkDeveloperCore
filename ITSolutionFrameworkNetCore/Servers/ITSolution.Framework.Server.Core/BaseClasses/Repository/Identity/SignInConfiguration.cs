using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository.Identity
{
    public class SignInConfiguration
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SignInConfiguration()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public TokenConfigurations()
        {
            Audience = "HunterAppAudience";
            Issuer = "HunterAppIssuer";
            Seconds = 10800;
        }
    }
}
