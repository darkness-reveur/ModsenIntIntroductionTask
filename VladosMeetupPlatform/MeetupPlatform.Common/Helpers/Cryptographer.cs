using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace MeetupPlatform.Common.Helpers
{
    public static class Cryptographer
    {
        public static byte[] Encrypt(string input, out byte[] salt)
        {
            salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Encrypt(input, salt);
        }

        public static byte[] Encrypt(string input, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 16);
        }
    }
}
