using System.Security.Cryptography;

namespace TaskManagementAPI.Helper
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            byte[] salt;
            byte[] buffer;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, 16, 10000))
            {
                salt = deriveBytes.Salt;
                buffer = deriveBytes.GetBytes(32);
            }

            byte[] dst = new byte[49];
            Buffer.BlockCopy(salt, 0, dst, 1, 16);
            Buffer.BlockCopy(buffer, 0, dst, 17, 32);

            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null)
                throw new ArgumentNullException(nameof(hashedPassword));
            if (providedPassword == null)
                throw new ArgumentNullException(nameof(providedPassword));

            byte[] src = Convert.FromBase64String(hashedPassword);

            byte[] salt = new byte[16];
            Buffer.BlockCopy(src, 1, salt, 0, 16);

            byte[] storedHash = new byte[32];
            Buffer.BlockCopy(src, 17, storedHash, 0, 32);

            using (var deriveBytes = new Rfc2898DeriveBytes(providedPassword, salt, 10000))
            {
                byte[] computedHash = deriveBytes.GetBytes(32);

                return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
            }
        }
    }
}
