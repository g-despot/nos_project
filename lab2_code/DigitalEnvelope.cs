using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab2_code
{
    /*      
     *      This class represents a digital envelope.
     */
    public class DigitalEnvelope
    {
        //The RSA algorithm used to encrypt the symmetric key.
        private RSAWrapper rsaAlgorithm;
        //The RSA algorithm name.
        private string rsaName;
        //The RSA algorithm key.
        private string rsaKey;
        //The symmetric algorithm used to encrypt the input.
        private SymmetricAlgorithmWrapper symmetricAlgorithm;
        //String representation of the symmetric algorithm.
        private string symmetricAlgorithmName;
        //The symmetric algorithm cypher.
        private CipherMode cipherMode;
        //String representation of the cypher.
        private string cipherName;
        //The initialization vector of the symmetric algorithm.
        private byte[] initializationVector;
        //The input file in byte array form.
        private byte[] input;

        //Create a DigitalEnvelope instance which will be used to decrypt an envelope.
        public DigitalEnvelope(string envelopePath, string privateKeyPath)
        {
            LoadEnvelope(envelopePath);
            LoadPrivateKey(privateKeyPath);
        }

        //Create a DigitalEnvelope instance which will be used to create an envelope.
        public DigitalEnvelope(SymmetricAlgorithmWrapper SYMAlgorithm, RSAWrapper rsaAlgorithm, string inputFilePath)
        {
            this.symmetricAlgorithm = SYMAlgorithm;
            this.rsaAlgorithm = rsaAlgorithm;
            LoadInputFile(inputFilePath);
        }

        //An object that contains the encrypted message and the encrypted symmetric key.
        public DigitalEnvelopeEncrypted DigitalEnvelopeEncrypted { get; set; }

        //Encrypt the input file and the symmetric key, save them as a file and return the DigitalEnvelopeEncrypted object 
        //containing the encrypted message and the encrypted symmetric key.
        public DigitalEnvelopeEncrypted CreateEnvelope()
        {
            DigitalEnvelopeEncrypted = new DigitalEnvelopeEncrypted
            (
                //Encrypt the input file. 
                symmetricAlgorithm.Encrypt(input),
                //Encrypt the symmetric key. 
                rsaAlgorithm.RSAEncrypt(symmetricAlgorithm.algorithm.Key, true)
            );
            SaveEnvelope();
            return DigitalEnvelopeEncrypted;
        }

        //Decrypt the envelope and return the output array.
        public byte[] DecryptEnvelope()
        {
            //Create the RSAWrapper from a string representation of its name.
            rsaAlgorithm = new RSAWrapper(rsaName);
            //Initialize the RSA algorithm key.
            rsaAlgorithm.RSAalg.FromXmlString(rsaKey);
            //Create the CipherMode from a string representation of its name.
            cipherMode = HelperExtensions.StringToCipherMode(cipherName);
            symmetricAlgorithm = HelperExtensions.StringToSymAlg(symmetricAlgorithmName, cipherMode, cipherName);
            symmetricAlgorithm.algorithm.IV = initializationVector;
            symmetricAlgorithm.algorithm.Key = rsaAlgorithm.RSADecrypt(DigitalEnvelopeEncrypted.EncryptedSymmetricKey, true);
            return symmetricAlgorithm.Decrypt(DigitalEnvelopeEncrypted.EncryptedMessage);
        }

        //Save envelope file.
        private void SaveEnvelope()
        {
            FileIO fileOutput = new FileIO(InitialParameters.basePath + InitialParameters.envelopeFilePath)
            {
                Description = "Envelope",
                FileName = "Envelope.txt",
                Method = symmetricAlgorithm.symmetricAlgorithmName + " " + symmetricAlgorithm.algorithm.KeySize + "\n\t" + rsaAlgorithm.rsaName,
                Cipher = symmetricAlgorithm.symmetricAlgorithmCipherName,
                InitializationVector = Convert.ToBase64String(symmetricAlgorithm.algorithm.IV),
                EnvelopeData = Convert.ToBase64String(DigitalEnvelopeEncrypted.EncryptedMessage),
                EnvelopeCryptKey = Convert.ToBase64String(DigitalEnvelopeEncrypted.EncryptedSymmetricKey)
            };
            fileOutput.SaveFile();
        }

        //Load envelope file.
        private void LoadEnvelope(string path)
        {
            FileIO fileInput = new FileIO(new StreamReader(path ?? InitialParameters.basePath + InitialParameters.envelopeFilePath));
            fileInput.LoadFile();
            string[] methods = fileInput.Method.Split('-');
            symmetricAlgorithmName = methods[0];
            rsaName = methods[1];
            cipherName = fileInput.Cipher;
            initializationVector = Convert.FromBase64String(fileInput.InitializationVector);
            DigitalEnvelopeEncrypted = new DigitalEnvelopeEncrypted
            (
                Convert.FromBase64String(fileInput.EnvelopeData),
                Convert.FromBase64String(fileInput.EnvelopeCryptKey)
            );
        }

        //Load private key file.
        private void LoadPrivateKey(string path)
        {
            FileIO fileInput = new FileIO(new StreamReader(path ?? InitialParameters.basePath + InitialParameters.privateKeyFilePath));
            fileInput.LoadFile();
            rsaKey = fileInput.RSAKey;
        }

        //Load input file.
        private void LoadInputFile(string path)
        {
            input = Encoding.Unicode.GetBytes(System.IO.File.ReadAllText(path ?? InitialParameters.basePath + InitialParameters.inputFilePath));
        }
    }
}