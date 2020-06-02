using System;
using System.Security.Cryptography;
using System.Security.Policy;

namespace lab2_code
{
    /*
            Wrapper class for the RSACryptoServiceProvider object.
            Used to add aditional properties such as HashAlgorithm and string representations 
            of the algorithm name ("RSA 1024", "RSA 2048", "RSA 3072") and the hash algorithm name ("SHA256", "SHA384", "SHA512").
        */
    public class RSAWrapper
    {
        //The RSA algorithm used to encrypt, decrypt, sign and verify data.
        public RSACryptoServiceProvider RSAalg;
        //String representation of the RSA algorithm.
        public string rsaName;
        //An object that holds the name of the hash algorithm.
        public HashAlgorithmName hashAlgorithmName;
        //The hash algorithm used to compute the message digest.
        public HashAlgorithm hashAlgorithm;

        //Create a RSAWrapper instance which will be used to encrypt and decrypt data.
        public RSAWrapper(string rsaName)
        {
            this.rsaName = rsaName;
            this.RSAalg = HelperExtensions.StringToRSA(rsaName);
        }

        //Create a RSAWrapper instance which will be used to sign and verify signed data.
        public RSAWrapper(string rsaName, HashAlgorithmName HashAlgorithmName)
        {
            this.rsaName = rsaName;
            this.RSAalg = HelperExtensions.StringToRSA(rsaName);
            this.hashAlgorithmName = HashAlgorithmName;
            this.hashAlgorithm = HashAlgorithm.Create(HashAlgorithmName.Name);
        }

        //Compute the message digest, sign it and return the signed data as a byte array.
        public byte[] HashAndSignBytes(byte[] DataToSign)
        {
            try
            {
                SavePublicKey();

                // Hash and sign the data. Pass a new instance of SHA1CryptoServiceProvider
                // to specify the use of SHA1 for hashing.
                return RSAalg.SignData(DataToSign, hashAlgorithm);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Return true if the signed data is valid.
        public bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData)
        {
            try
            {
                // Verify the data using the signature.  Pass a new instance of SHA1CryptoServiceProvider
                // to specify the use of SHA1 for hashing.
                return RSAalg.VerifyData(DataToVerify, hashAlgorithm, SignedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Encrypt data with a RSA algorithm and return it as a byte array.
        public byte[] RSAEncrypt(byte[] DataToEncrypt, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                SavePrivateKey();

                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                encryptedData = RSAalg.Encrypt(DataToEncrypt, DoOAEPPadding);

                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Decrypt data with a RSA algorithm and return it as a byte array.
        public byte[] RSADecrypt(byte[] DataToDecrypt, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;

                //Decrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                decryptedData = RSAalg.Decrypt(DataToDecrypt, DoOAEPPadding);
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        //Save private key file.
        private void SavePrivateKey()
        {
            FileIO fileOutput = new FileIO(InitialParameters.basePath + InitialParameters.privateKeyFilePath)
            {
                Description = "Private key",
                FileName = "Private key.txt",

                //Include private parameters of the key.
                RSAKey = RSAalg.ToXmlString(true)
            };
            fileOutput.SaveFile();
        }

        //Save public key file.
        private void SavePublicKey()
        {
            FileIO fileOutput = new FileIO(InitialParameters.basePath + InitialParameters.publicKeyFilePath)
            {
                Description = "Public key",
                FileName = "Public key.txt",

                //Don't include private parameters of the key.
                RSAKey = RSAalg.ToXmlString(false)
            };
            fileOutput.SaveFile();
        }
    }
}
