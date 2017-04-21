using System;
using System.Text;
using System.Security.Cryptography;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Application.Settings
{
    public static class UserCredentialsEncryptService
    {
        public static UserCredentials Protect(UserCredentials userCredentials)
        {
            if (userCredentials == null)
            {
                return null;
            }

            string protectedPassword = null;

            if (userCredentials.Password != null)
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(userCredentials.Password);
                byte[] protectedPasswordBytes = ProtectedData.Protect(passwordBytes, null);
                protectedPassword = Convert.ToBase64String(protectedPasswordBytes);
            }

            return new UserCredentials
            {
                Username = userCredentials.Username,
                Password = protectedPassword,
                ServerUri = userCredentials.ServerUri
            };
        }

        public static UserCredentials Unprotect(UserCredentials userCredentials)
        {
            if (userCredentials == null)
            {
                return null;
            }

            string password = null;

            if (userCredentials.Password != null)
            {
                byte[] protectedPassword = Convert.FromBase64String(userCredentials.Password);
                byte[] paswordBytes = ProtectedData.Unprotect(protectedPassword, null);
                password = Encoding.UTF8.GetString(paswordBytes, 0, paswordBytes.Length);
            }

            return new UserCredentials
                       {
                           Username = userCredentials.Username,
                           Password = password,
                           ServerUri = userCredentials.ServerUri
                       };
        }
    }
}
