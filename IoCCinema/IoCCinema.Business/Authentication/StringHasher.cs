using System;
using System.Security.Cryptography;
using System.Text;

namespace IoCCinema.Business.Authentication
{
    public class StringHasher
    {
        public string GetHash(string password, string salt)
        {
            byte[] unhashedBytes = Encoding.Unicode.GetBytes(string.Concat(salt, password));

            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);

            return Convert.ToBase64String(hashedBytes);
        }

        public bool CompareHash(string attemptedPassword, string hash, string salt)
        {
            return hash == GetHash(attemptedPassword, salt);
        }
    }
}
