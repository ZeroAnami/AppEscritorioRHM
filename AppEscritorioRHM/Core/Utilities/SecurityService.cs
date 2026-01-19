using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AppEscritorioRHM.Core.Utilities
{
    public static class SecurityService
    {
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return null;

            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                byte[] encryptedBytes = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);

                return Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string Decrypt(string encryptedBase64)
        {
            if (string.IsNullOrEmpty(encryptedBase64)) return null;

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);

                byte[] plainBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);

                return Encoding.UTF8.GetString(plainBytes);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
