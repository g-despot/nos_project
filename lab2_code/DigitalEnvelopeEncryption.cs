using System.Linq;

namespace lab2_code
{
    /*      
     *      This class combines the enrypted envelope message and the encrypted symmetric key.
     */
    public class DigitalEnvelopeEncrypted
    {
        public DigitalEnvelopeEncrypted(byte[] encryptedMessage, byte[] encryptedSymmetricKey)
        {
            this.EncryptedMessage = encryptedMessage;
            this.EncryptedSymmetricKey = encryptedSymmetricKey;
        }

        public byte[] EncryptedMessage { get; set; }
        public byte[] EncryptedSymmetricKey { get; set; }

        //Return a byte array of the encrypted message and encrypted symmetric key.
        public byte[] EncryptedDataKeyPair()
        {
            return EncryptedMessage.Concat(EncryptedSymmetricKey).ToArray();
        }
    }
}
