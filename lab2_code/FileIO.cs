using System.IO;
using System.Text;

namespace lab2_code
{
    /*      
     *      This class is used to save and load files with predefined properties.
     */
    class FileIO
    {
        private readonly StreamWriter streamWriter;
        private readonly StreamReader streamReader;

        //Constructor to save a file to the given path.
        public FileIO(string path)
        {
            this.streamWriter = new StreamWriter(path);
        }

        //Constructor to load a file with the given StreamReader.
        public FileIO(StreamReader streamReader)
        {
            this.streamReader = streamReader;
        }

        //Check which properties have been initialized in this class instance and save them as a file.
        public void SaveFile()
        {
            streamWriter.WriteLine("---BEGIN OS2 CRYPTO DATA---");

            if (Description != null && Description != "")
            {
                streamWriter.WriteLine("Description:");
                streamWriter.WriteLine("\t" + Description + "\n");
            }

            if (FileName != null && FileName != "")
            {
                streamWriter.WriteLine("File Name:");
                streamWriter.WriteLine("\t" + FileName + "\n");
            }

            if (Method != null && Method != "")
            {
                streamWriter.WriteLine("Method:");
                streamWriter.WriteLine("\t" + Method + "\n");
            }

            if (Cipher != null && Cipher != "")
            {
                streamWriter.WriteLine("Cipher:");
                streamWriter.WriteLine("\t" + Cipher + "\n");
            }

            if (KeyLnegth != null && KeyLnegth != "")
            {
                streamWriter.WriteLine("Key lenegth:");
                streamWriter.WriteLine("\t" + KeyLnegth + "\n");
            }

            if (SecretKey != null && SecretKey != "")
            {
                streamWriter.WriteLine("Secret key:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(SecretKey, 60));
            }

            if (InitializationVector != null && InitializationVector != "")
            {
                streamWriter.WriteLine("Initialization vector:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(InitializationVector, 60));
            }

            if (Modulus != null && Modulus != "")
            {
                streamWriter.WriteLine("Modulus:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(Modulus, 60));
            }

            if (PublicExponent != null && PublicExponent != "")
            {
                streamWriter.WriteLine("Public exponent:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(PublicExponent, 60));
            }

            if (PrivateExponent != null && PrivateExponent != "")
            {
                streamWriter.WriteLine("Private exponent:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(PrivateExponent, 60));
            }

            if (Signature != null && Signature != "")
            {
                streamWriter.WriteLine("Signature:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(Signature, 60));
            }

            if (Data != null && Data != "")
            {
                streamWriter.WriteLine("Data:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(Data, 60));
            }

            if (EnvelopeData != null && EnvelopeData != "")
            {
                streamWriter.WriteLine("Envelope data:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(EnvelopeData, 60));
            }

            if (EnvelopeCryptKey != null && EnvelopeCryptKey != "")
            {
                streamWriter.WriteLine("Envelope crypt key:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(EnvelopeCryptKey, 60));
            }

            if (RSAKey != null && RSAKey != "")
            {
                streamWriter.WriteLine("RSA key:");
                HelperExtensions.PrintStringArray(streamWriter, HelperExtensions.SpliceText(RSAKey, 60));
            }

            streamWriter.WriteLine("---END OS2 CRYPTO DATA---");
            streamWriter.Close();
        }

        //Check which properties are present in the file and load them to this class instance.
        public void LoadFile()
        {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            string line = streamReader.ReadLine();
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            while (!(line = streamReader.ReadLine().Trim()).Equals("---END OS2 CRYPTO DATA---"))
            {
                if (line.Equals("Description:"))
                {
                    Description = streamReader.ReadLine().Trim();
                    continue;
                }
                else if (line.Equals("File Name:"))
                {
                    FileName = streamReader.ReadLine().Trim();
                    continue;
                }
                else if (line.Equals("Method:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                        sb.Append("-");
                    }
                    Method = sb.ToString();
                    continue;
                }
                else if (line.Equals("Cipher:"))
                {
                    Cipher = streamReader.ReadLine().Trim();
                    continue;
                }
                else if (line.Equals("Key lenegth:"))
                {
                    KeyLnegth = streamReader.ReadLine().Trim();
                    continue;
                }
                else if (line.Equals("Secret key:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    SecretKey = sb.ToString();
                    continue;
                }
                else if (line.Equals("Initialization vector:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    InitializationVector = sb.ToString();
                    continue;
                }
                else if (line.Equals("Modulus:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    Modulus = sb.ToString();
                    continue;
                }
                else if (line.Equals("Public exponent:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    PublicExponent = sb.ToString();
                    continue;
                }
                else if (line.Equals("Private exponent:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    PrivateExponent = sb.ToString();
                    continue;
                }
                else if (line.Equals("Signature:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    Signature = sb.ToString();
                    continue;
                }
                else if (line.Equals("Data:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    Data = sb.ToString();
                    continue;
                }
                else if (line.Equals("Envelope data:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    EnvelopeData = sb.ToString();
                    continue;
                }
                else if (line.Equals("Envelope crypt key:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    EnvelopeCryptKey = sb.ToString();
                    continue;
                }
                else if (line.Equals("RSA key:"))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!(line = streamReader.ReadLine().Trim()).Equals(""))
                    {
                        sb.Append(line);
                    }
                    RSAKey = sb.ToString();
                    continue;
                }
            }
            streamReader.Close();
        }

        //All the available file properties.
        public string Description { get; set; }
        public string FileName { get; set; }
        public string Method { get; set; }
        public string KeyLnegth { get; set; }
        public string SecretKey { get; set; }
        public string InitializationVector { get; set; }
        public string Modulus { get; set; }
        public string PublicExponent { get; set; }
        public string PrivateExponent { get; set; }
        public string Signature { get; set; }
        public string Data { get; set; }
        public string EnvelopeData { get; set; }
        public string EnvelopeCryptKey { get; set; }
        public string Cipher { get; set; }
        public string RSAKey { get; set; }
    }
}
