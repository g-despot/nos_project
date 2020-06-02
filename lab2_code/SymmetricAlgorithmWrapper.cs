using System;
using System.Security.Cryptography;

namespace lab2_code
{
    /*
        Wrapper class for the SymmetricAlgorithm object.
        Used to add aditional properties such as CipherMode and string representations 
        of the algorithm name ("Aes", "TripleDES") and cipher name ("CBC", "ECB").
    */
    public class SymmetricAlgorithmWrapper
    {
        //String representation of the symmetric algorithm.
        public string symmetricAlgorithmName;
        //String representation of the cipher.
        public string symmetricAlgorithmCipherName;
        //The symmetric algorithm used to encrypt and decrypt data.
        public SymmetricAlgorithm algorithm;

        //Create the SymmetricAlgorithm object given its name, key size and the cipher.
        public SymmetricAlgorithmWrapper(string symmetricAlgorithmName, string symmetricAlgorithmCipherName, int keySize, CipherMode cipherMode)
        {
            this.symmetricAlgorithmName = symmetricAlgorithmName;
            this.symmetricAlgorithmCipherName = symmetricAlgorithmCipherName;
            this.algorithm = SymmetricAlgorithm.Create(symmetricAlgorithmName);
            this.algorithm.KeySize = keySize;
            this.algorithm.Mode = cipherMode;
            this.algorithm.Padding = PaddingMode.PKCS7;
        }

        //Encrypt input array.
        public byte[] Encrypt(byte[] input)
        {
            try
            {
                ICryptoTransform cryptoTransform = algorithm.CreateEncryptor();
                byte[] output = cryptoTransform.TransformFinalBlock(input, 0, input.Length);
                return output;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Decrypt input array.
        public byte[] Decrypt(byte[] input)
        {
            try
            {
                ICryptoTransform cryptoTransform = algorithm.CreateDecryptor();
                byte[] output = cryptoTransform.TransformFinalBlock(input, 0, input.Length);
                return output;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}