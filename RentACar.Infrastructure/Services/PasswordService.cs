using Microsoft.Extensions.Options;
using RentACar.Infrastructure.Interfaces;
using RentACar.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions options;
        public PasswordService(IOptions<PasswordOptions> options)
        {
            this.options = options.Value;
        }
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3) {
                throw new FormatException("Unexpected hash format");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
               password,
               salt,
               iterations,
               HashAlgorithmName.SHA512
               ))
            {
                var keyToCheck = algorithm.GetBytes(options.KeySize);
                
                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            using(var algorithm = new Rfc2898DeriveBytes(
                password, 
                options.SaltSize,
                options.Iterations,
                HashAlgorithmName.SHA512
                ))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{options.Iterations}.{salt}.{key}";
            }
        }
    }
}
