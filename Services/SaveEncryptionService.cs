using System;
using System.Text;

namespace YouthMeadowGeneralStore.Services
{
    public sealed class SaveEncryptionService
    {
        private readonly byte[] _keyBytes;

        public SaveEncryptionService(string key)
        {
            _keyBytes = Encoding.UTF8.GetBytes(key ?? string.Empty);
        }

        public string Encrypt(string plainText)
        {
            var dataBytes = Encoding.UTF8.GetBytes(plainText ?? string.Empty);
            var cipherBytes = Xor(dataBytes);
            var builder = new StringBuilder(cipherBytes.Length * 2);

            foreach (var value in cipherBytes)
            {
                builder.Append(value.ToString("x2"));
            }

            return builder.ToString();
        }

        public string Decrypt(string hexText)
        {
            if (string.IsNullOrWhiteSpace(hexText))
            {
                return string.Empty;
            }

            var buffer = new byte[hexText.Length / 2];
            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(hexText.Substring(i * 2, 2), 16);
            }

            return Encoding.UTF8.GetString(Xor(buffer));
        }

        private byte[] Xor(byte[] bytes)
        {
            if (_keyBytes.Length == 0)
            {
                return bytes;
            }

            var result = new byte[bytes.Length];
            for (var i = 0; i < bytes.Length; i++)
            {
                result[i] = (byte)(bytes[i] ^ _keyBytes[i % _keyBytes.Length]);
            }

            return result;
        }
    }
}
