using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VladosMeetupPlatform.API.Auth
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "MyAuthClient";
        public const int LifeTime = 60; // minutes

        private const string Key = "mysupersecret_secretkey!123";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
