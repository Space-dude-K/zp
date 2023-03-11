using NLog;
using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Zp.Crypto
{
    static class Encryptor
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private static readonly int saltLengthLimit = 32;
        private static byte[] GetSalt()
        {
            return GetSalt(saltLengthLimit);
        }
        public static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        public static string EncryptString(SecureString input, byte[] salt)
        {
            byte[] encryptedData = ProtectedData.Protect(
                Encoding.Unicode.GetBytes(ToInsecureString(input)),
                salt,
                DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }
        public static SecureString DecryptString(string encryptedData, string salt)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    Convert.FromBase64String(salt),
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);

                return ToSecureString(Encoding.Unicode.GetString(decryptedData));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
                return new SecureString();
            }
        }
        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();

            foreach (char c in input)
            {
                secure.AppendChar(c);
            }

            secure.MakeReadOnly();
            return secure;
        }
        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);

            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }

            return returnValue;
        }
    }
}