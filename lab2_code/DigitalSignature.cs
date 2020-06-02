using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab2_code
{
    /*      
     *      This class represents a digital signature.
     */
    public class DigitalSignature
    {
        //The RSA algorithm used to encrypt the symmetric key.
        private RSAWrapper rsaAlgorithm;
        //String representation of the RSA algorithm.
        private string rsaName;
        //The RSA algorithm key.
        private string rsaKey;
        //The hash algorithm used to generate the message digest.
        private HashAlgorithmName hashAlgorithmName;
        //The digital signature in byte array form.
        private byte[] signature;
        //The input file in byte array form.
        private byte[] input;

        //Constructor for the purpose of validating signatures. The input is given as a file path.
        public DigitalSignature(string inputFilePath, string signatureFilePath, string publicKeyFilePath)
        {
            LoadInputFile(inputFilePath);
            LoadSignature(signatureFilePath);
            LoadPublicKey(publicKeyFilePath);
        }

        //Constructor for the purpose of validating signatures. The input is given as a byte array.
        public DigitalSignature(byte[] input, string signatureFilePath, string publicKeyFilePath)
        {
            this.input = input;
            LoadSignature(signatureFilePath);
            LoadPublicKey(publicKeyFilePath);
        }

        //Constructor for the purpose of signing files. The input is given as a file path.
        public DigitalSignature(RSAWrapper rsaAlgorithm, string inputFilePath)
        {
            this.rsaAlgorithm = rsaAlgorithm;
            LoadInputFile(inputFilePath);
        }

        //Constructor for the purpose of signing files.  The input is given as a byte array.
        public DigitalSignature(RSAWrapper rsaAlgorithm, byte[] input)
        {
            this.rsaAlgorithm = rsaAlgorithm;
            this.input = input;
        }

        //Create the digital signature, save it and return it.
        public byte[] CreateSignature()
        {
            signature = rsaAlgorithm.HashAndSignBytes(input);
            SaveSignature(signature);
            return signature;
        }

        //Validate the digital signature and return true if was authenticated successfully.
        public bool AuthenticateSignature()
        {
            rsaAlgorithm = new RSAWrapper(rsaName, hashAlgorithmName);
            rsaAlgorithm.RSAalg.FromXmlString(rsaKey);
            return rsaAlgorithm.VerifySignedHash(input, signature);
        }

        //Save the signature file.
        private void SaveSignature(byte[] signature)
        {
            FileIO fileOutput = new FileIO(InitialParameters.basePath + InitialParameters.signatureFilePath)
            {
                Description = "Signature",
                FileName = "Signature.txt",
                Method = rsaAlgorithm.hashAlgorithmName.Name + "\n\t" + rsaAlgorithm.rsaName,
                Signature = Convert.ToBase64String(signature)
            };
            fileOutput.SaveFile();
        }

        //Load the signature file.
        private void LoadSignature(string path)
        {
            FileIO fileInput = new FileIO(new StreamReader(path ?? InitialParameters.basePath + InitialParameters.signatureFilePath));
            fileInput.LoadFile();
            string[] methods = fileInput.Method.Split('-');
            hashAlgorithmName = new HashAlgorithmName(methods[0]);
            rsaName = methods[1];
            signature = Convert.FromBase64String(fileInput.Signature);
        }

        //Load the public key file.
        private void LoadPublicKey(string path)
        {
            FileIO fileInput = new FileIO(new StreamReader(path ?? InitialParameters.basePath + InitialParameters.publicKeyFilePath));
            fileInput.LoadFile();
            rsaKey = fileInput.RSAKey;
        }

        //Load the input file.
        private void LoadInputFile(string path)
        {
            input = Encoding.Unicode.GetBytes(System.IO.File.ReadAllText(path ?? InitialParameters.basePath + InitialParameters.inputFilePath));
        }
    }
}