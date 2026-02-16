using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AppEscritorioRHM.Core.Utilities
{
    public static class SecurityService
    {
        private const int SaltSize = 16; // 128 bits
        private const int KeySize = 32;  // 256 bits
        private const int IvSize = 16;   // 128 bits (Bloque AES)
        private const int Iterations = 300000;
        private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;
        private const char SegmentDelimiter = ':';

        /// <summary>
        /// Encripta un texto plano usando AES con una contraseña proporcionada.
        /// </summary>
        /// <param name="plainText">Texto a encriptar</param>
        /// <param name="password">Contraseña necesaria para desencriptar posteriormente mediante el método Decrypt</param>
        /// <returns>Texto encriptado en base64</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Encrypt(string plainText, string password)
        {
            if (string.IsNullOrEmpty(plainText)) return null;
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            try
            {
                byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
                byte[] key = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    Iterations,
                    HashAlgorithmName.SHA256,
                    KeySize);

                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.GenerateIV();
                    byte[] iv = aes.IV;

                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream())
                    {
                        // Escribimos primero el Salt y el IV
                        ms.Write(salt, 0, salt.Length);
                        ms.Write(iv, 0, iv.Length);

                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Desencripta un texto cifrado en base64 usando AES con la contraseña que se utilizó para encriptarse.
        /// </summary>
        /// <param name="encryptedBase64"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Decrypt(string encryptedBase64, string password)
        {
            if (string.IsNullOrEmpty(encryptedBase64)) return null;
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            try
            {
                byte[] fullCipher = Convert.FromBase64String(encryptedBase64);

                // Validar longitud mínima
                if (fullCipher.Length < SaltSize + IvSize) return null;

                byte[] salt = new byte[SaltSize];
                byte[] iv = new byte[IvSize];
                byte[] cipherText = new byte[fullCipher.Length - SaltSize - IvSize];

                // Extraer partes
                Array.Copy(fullCipher, 0, salt, 0, SaltSize);
                Array.Copy(fullCipher, SaltSize, iv, 0, IvSize);
                Array.Copy(fullCipher, SaltSize + IvSize, cipherText, 0, cipherText.Length);

                byte[] key = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    Iterations,
                    HashAlgorithmName.SHA256,
                    KeySize);

                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream(cipherText))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Genera un hash seguro para la contraseña proporcionada.
        /// Al proporcionar este hash al método Verify verifica si la contraseña proporcionada es la que se encuentra dentro del hash.
        /// </summary>
        /// <param name="password">Contraseña utilizada para generar el hash</param>
        /// <returns>Contraseña hasheada en base64 dentro de 3 datos concatenados con dos puntos "iteraciones:sal:hash"</returns>
        public static string Hash(string password)
        {
            // Crear la Sal aleatoria
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Derivar la llave (Hash)
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                _algorithm,
                KeySize
            );

            // Formatear para almacenamiento: iteraciones:sal:hash
            // Convertimos a Base64 para guardar como texto
            return string.Join(
                SegmentDelimiter,
                Iterations,
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash)
            );
        }
        /// <summary>
        /// Verifica si la contraseña proporcionada coincide con el hash.
        /// </summary>
        /// <param name="password">Contraseña utilizada para el hash</param>
        /// <param name="hash">Hash generado mediante el método Hash</param>
        /// <returns>Devuelve true si la contraseña se encuentra dentro del hash, false en caso contrario.</returns>
        public static bool Verify(string password, string hash)
        {
            // Descomponer el hash guardado
            string[] segments = hash.Split(SegmentDelimiter);

            // Validación básica de formato
            if (segments.Length != 3) return false;

            int iterations = int.Parse(segments[0]);
            byte[] salt = Convert.FromBase64String(segments[1]);
            byte[] originalHash = Convert.FromBase64String(segments[2]);

            // Recalcular el hash con la contraseña entrante y la SAL original
            byte[] newHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                _algorithm,
                KeySize
            );

            // Comparación segura (Constant Time Comparison)
            // Usamos FixedTimeEquals para evitar ataques de tiempo (Timing Attacks)
            return CryptographicOperations.FixedTimeEquals(originalHash, newHash);
        }

    }
}
