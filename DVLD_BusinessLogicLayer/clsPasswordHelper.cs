using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics; 

namespace DVLD_BusinessLogicLayer {

   
    public class clsPasswordHelper {

        private const int SaltSize = 32;
        private const int HashSize = 32;
        //private const int Iterations = 600000; 
        private const int Iterations = 200000; // hashing here takes 2.2s 

        public static string HashPassword(string password) {
            byte[] salt = new byte[SaltSize];

            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(salt);
            }

            byte[] hash;

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password, salt, Iterations, HashAlgorithmName.SHA256)) {
                hash = pbkdf2.GetBytes(HashSize);

                // Store salt and hash together.
                return Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string storedPassword) {
            string[] parts = storedPassword.Split(':');

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHash = Convert.FromBase64String(parts[1]);

            byte[] computedHash;

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password, salt, Iterations, HashAlgorithmName.SHA256)) {
                computedHash = pbkdf2.GetBytes(HashSize);
            }

            return FixedTimeEquals(storedHash, computedHash);
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b) {
            if (a.Length != b.Length)
                return false;

            int result = 0;

            for (int i = 0; i < a.Length; i++)
                result |= a[i] ^ b[i];

            return result == 0;
        }
    }

}
