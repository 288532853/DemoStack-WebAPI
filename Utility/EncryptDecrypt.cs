﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public class EncryptDecrypt
    {
        private static string defaultKeyPassPhrase = "hey1ie4o4"; // can be any string
        private static string defaultSaltValue = "8172hey87"; // can be any string
        private static string hashAlgorithm = "MD5"; // can be "MD5"SHA1
        private static int passwordIterations = 2; // can be any number
        private static string defaultIV = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        private static int keySize = 256; // can be 192 or 128 or 256


        public static string Encrypt(string plainText)
        {
            return Encrypt(plainText, defaultKeyPassPhrase);
        }

        public static string Encrypt(string plainText, string key)
        {
            return Encrypt(plainText, key, defaultSaltValue);
        }

        public static string Encrypt(string plainText, string key, string salt)
        {
            return Encrypt(plainText, key, salt, defaultIV);
        }

        public static string Encrypt(string plainText, string key, string salt, string IV)
        {
            // Set defaults if empty
            if (string.IsNullOrEmpty(key)) key = defaultKeyPassPhrase;
            if (string.IsNullOrEmpty(IV)) IV = defaultIV;
            if (string.IsNullOrEmpty(salt)) salt = defaultSaltValue;

            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(IV);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                key,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                keyBytes,
                initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                encryptor,
                CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }


        // DECRYPTION RELATED

        public static string Decrypt(string encryptedText)
        {
            return Decrypt(encryptedText, defaultKeyPassPhrase);
        }

        public static string Decrypt(string encryptedText, string key)
        {
            return Decrypt(encryptedText, key, defaultSaltValue);
        }

        public static string Decrypt(string encryptedText, string key, string salt)
        {
            return Decrypt(encryptedText, key, salt, defaultIV);
        }

        public static string Decrypt(string encryptedText, string key, string salt, string IV)
        {
            string plainText = string.Empty;
            if (!string.IsNullOrEmpty(encryptedText))
            {
                // Set defaults if values are null or empty
                if (string.IsNullOrEmpty(key)) key = defaultKeyPassPhrase;
                if (string.IsNullOrEmpty(salt)) salt = defaultSaltValue;
                if (string.IsNullOrEmpty(IV)) IV = defaultIV;

                encryptedText = encryptedText.Replace(" ", "+");
                // Convert strings defining encryption key characteristics into byte
                // arrays. Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8
                // encoding.
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(IV);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);

                // Convert our ciphertext into a byte array.
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

                // First, we must create a password, from which the key will be 
                // derived. This password will be generated from the specified 
                // passphrase and salt value. The password will be created using
                // the specified hash algorithm. Password creation can be done in
                // several iterations.
                PasswordDeriveBytes password = new PasswordDeriveBytes(
                    key,
                    saltValueBytes,
                    hashAlgorithm,
                    passwordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                byte[] keyBytes = password.GetBytes(keySize / 8);

                // Create uninitialized Rijndael encryption object.
                RijndaelManaged symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate decryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                    keyBytes,
                    initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                // Define cryptographic stream (always use Read mode for encryption).
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                             decryptor,
                                                             CryptoStreamMode.Read);

                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold ciphertext;
                // plaintext is never longer than ciphertext.
               // byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                byte[] plainTextBytes = new byte[cipherTextBytes.LongLength];

                // Start decrypting.
                int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                           0,
                                                           plainTextBytes.Length);

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert decrypted data into a string. 
                // Let us assume that the original plaintext string was UTF8-encoded.
                plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                           0,
                                                           decryptedByteCount);
            }
            // Return decrypted string.   
            return plainText;
        }
    }//end class
}//end namespace