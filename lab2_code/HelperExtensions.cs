using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace lab2_code
{
    /*
     *      This class contains helper methods used throughout the project.
     */
    public class HelperExtensions
    {
        //Divide a string into multiple lines depending on the maximum line length.
        public static string[] SpliceText(string text, int lineLength)
        {
            return Regex.Matches(text, ".{1," + lineLength + "}").Cast<Match>().Select(m => m.Value).ToArray();
        }

        //Print each string array element in a next line with a tab at the beginning.
        public static void PrintStringArray(StreamWriter streamWriter, string[] array)
        {
            foreach (string tmp in array)
            {
                streamWriter.WriteLine("\t" + tmp);
            }
            streamWriter.WriteLine();
        }

        //Return a SymmetricAlgorithmWrapper object given the symmetric algorithm name (including key size), CipherMode and cipher name.
        public static SymmetricAlgorithmWrapper StringToSymAlg(string input, CipherMode cipherMode, string cipherName)
        {
            SymmetricAlgorithmWrapper symmetricAlgorithm = input switch
            {
                "AES 128" => new SymmetricAlgorithmWrapper("Aes", cipherName, 128, cipherMode),
                "AES 192" => new SymmetricAlgorithmWrapper("Aes", cipherName, 192, cipherMode),
                "AES 256" => new SymmetricAlgorithmWrapper("Aes", cipherName, 256, cipherMode),
                "TripleDES 128" => new SymmetricAlgorithmWrapper("TripleDES", cipherName, 128, cipherMode),
                "TripleDES 192" => new SymmetricAlgorithmWrapper("TripleDES", cipherName, 192, cipherMode),
                _ => new SymmetricAlgorithmWrapper("Aes", cipherName, 128, cipherMode),
            };
            return symmetricAlgorithm;
        }

        //Return a RSACryptoServiceProvider object given the RSA algorithm key size.
        public static RSACryptoServiceProvider StringToRSA(string input)
        {
            RSACryptoServiceProvider RSAalg = input switch
            {
                "RSA 1024" => new RSACryptoServiceProvider(1024),
                "RSA 2048" => new RSACryptoServiceProvider(2048),
                "RSA 3072" => new RSACryptoServiceProvider(3072),
                _ => new RSACryptoServiceProvider(1024),
            };
            return RSAalg;
        }

        //Return a CipherMode object given the cipher name.
        public static CipherMode StringToCipherMode(string input)
        {
            var cipherMode = input switch
            {
                "CBC" => CipherMode.CBC,
                "ECB" => CipherMode.ECB,
                "CFB" => CipherMode.CFB,
                "OFB" => CipherMode.OFB,
                "CTS" => CipherMode.CTS,
                _ => CipherMode.CBC,
            };
            return cipherMode;
        }

        //Return a HashAlgorithmName object given the hash algorithm name (including key size). 
        public static HashAlgorithmName StringToHashAlgorithm(string input)
        {
            var hashAlgorithmName = input switch
            {
                "SHA256" => HashAlgorithmName.SHA256,
                "SHA384" => HashAlgorithmName.SHA384,
                "SHA512" => HashAlgorithmName.SHA512,
                _ => HashAlgorithmName.SHA256,
            };
            return hashAlgorithmName;
        }
    }
}
