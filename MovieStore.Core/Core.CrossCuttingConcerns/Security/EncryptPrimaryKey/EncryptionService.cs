using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Security.EncryptPrimaryKey
{
    public static class EncryptionService
    {
        private static string EncryptionKey;
        public static void Initialize(IConfiguration configuration)
        {
            EncryptionKey = configuration["Key"];
        }

        public static string Encrypt(int id)
        {
            byte[] idBytes = BitConverter.GetBytes(id);
            byte[] keyBytes = Encoding.UTF8.GetBytes(EncryptionKey);

            for (int i = 0; i < idBytes.Length; i++)
            {
                idBytes[i] ^= keyBytes[i % keyBytes.Length];
            }

            return Convert.ToBase64String(idBytes);
        }

        public static int Decrypt(string encryptedId)
        {
            byte[] idBytes = Convert.FromBase64String(encryptedId);
            byte[] keyBytes = Encoding.UTF8.GetBytes(EncryptionKey);

            for (int i = 0; i < idBytes.Length; i++)
            {
                idBytes[i] ^= keyBytes[i % keyBytes.Length];
            }

            return BitConverter.ToInt32(idBytes, 0);
        }
    }
}
