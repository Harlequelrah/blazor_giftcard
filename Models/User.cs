using System;
using System.Security.Cryptography;
using System.Text;
namespace blazor_giftcard.Models

{
    public class User
    {
        public int Id { get; set; }

        public string NomRole { get; set; }

        public string NomComplet { get; set; }

        public string Email { get; set; }
        public string Telephone { get; set; }
        public string DateInscription { get; set; }
        public string Adresse { get; set; }
        public bool IsActive { get; set; }

    }
    public class Admin
    {
        private static readonly Random _random = new Random();
        public string NomComplet { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; } = GenerateSecurePassword(12);

        public static string GenerateSecurePassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=<>?";
            StringBuilder password = new StringBuilder();
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[4];
                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(randomBytes);
                    int index = BitConverter.ToInt32(randomBytes, 0) % validChars.Length;
                    if (index < 0) index = -index;
                    password.Append(validChars[index]);
                }
            }
            return password.ToString();
        }

    }


}
