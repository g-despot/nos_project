using lab2_code;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace lab2_ui
{
    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Initialize file paths for the Digital Envelope tab.
            DEInputTextBox.Text = InitialParameters.basePath + InitialParameters.inputFilePath;
            DEInputEnvelopeTextBox.Text = InitialParameters.basePath + InitialParameters.envelopeFilePath;
            DEInputPrivateKeyTextBox.Text = InitialParameters.basePath + InitialParameters.privateKeyFilePath;

            //Initialize file paths for the Digital Signature tab.
            DSInputTextBox.Text = InitialParameters.basePath + InitialParameters.inputFilePath;
            DSInputSignatureTextBox.Text = InitialParameters.basePath + InitialParameters.signatureFilePath;
            DSInputPublicKeyTextBox.Text = InitialParameters.basePath + InitialParameters.publicKeyFilePath;

            //Initialize file paths for the Digital Certificate tab.
            DCInputTextBox.Text = InitialParameters.basePath + InitialParameters.inputFilePath;
            DCInputSignatureTextBox.Text = InitialParameters.basePath + InitialParameters.signatureFilePath;
            DCInputSignaturePublicKeyTextBox.Text = InitialParameters.basePath + InitialParameters.publicKeyFilePath;
            DCInputEnvelopeTextBox.Text = InitialParameters.basePath + InitialParameters.envelopeFilePath;
            DCInputEnvelopePrivateKeyTextBox.Text = InitialParameters.basePath + InitialParameters.privateKeyFilePath;
        }

        //Select input file.
        private void DEInputButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DEInputTextBox, DEInputTextBlock);
        }

        //Select envelope file.
        private void DESelectEnvelopeButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DEInputEnvelopeTextBox, DEInputTextBlock);
        }

        //Select private key file.
        private void DESelectPrivateKeyButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DEInputPrivateKeyTextBox, DEInputTextBlock);
        }

        //Generate a digital envelope.
        private void DEGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            //Create a CipherMode object using the dropdown box selection.
            CipherMode cipherMode = HelperExtensions.StringToCipherMode(DESymTypeDropdown.SelectedItem.ToString());
            //Create a SymmetricAlgorithmWrapper object using the dropdown box selections and the CipherMode object.
            SymmetricAlgorithmWrapper symmetricAlgorithmType = HelperExtensions.StringToSymAlg(DESymDropdown.SelectedItem.ToString(), cipherMode, DESymTypeDropdown.SelectedItem.ToString());
            //Create a RSAWrapper object using the dropdown box selection.
            RSAWrapper rsaAlgorithm = new RSAWrapper(DERsaDropdown.SelectedItem.ToString());

            DigitalEnvelope digitalEnvelope = new DigitalEnvelope(symmetricAlgorithmType, rsaAlgorithm, DEInputTextBox.Text);
            digitalEnvelope.CreateEnvelope();
            DEInputTextBlock.Text = "Envelope generated successfully!";
        }

        private void DEDecryptButton_Click(object sender, RoutedEventArgs e)
        {
            //Create a DigitalEnvelope object that will load all the necessary properties from saved files.
            DigitalEnvelope digitalEnvelope = new DigitalEnvelope(DEInputEnvelopeTextBox.Text, DEInputPrivateKeyTextBox.Text);
            byte[] result = digitalEnvelope.DecryptEnvelope();
            //If the result of the decryption is null an error must have occurred.
            DEInputTextBlock.Text = result != null ? Encoding.Unicode.GetString(result) : "Envelope could not be decrypted!";
        }

        //Select input file.
        private void DSInputButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DSInputTextBox, DSInputTextBlock);
        }

        //Select signature file.
        private void DSInputSignatureButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DSInputSignatureTextBox, DSInputTextBlock);
        }

        //Select public key file.
        private void DSPublicKeyButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DSInputPublicKeyTextBox, DSInputTextBlock);
        }

        //Generate a signature.
        private void DSGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            //Create a HashAlgorithmName object using the dropdown box selection.
            HashAlgorithmName hashAlgorithmName = HelperExtensions.StringToHashAlgorithm(DEHashDropdown.SelectedItem.ToString());
            //Create a RSAWrapper object using the dropdown box selection and the HashAlgorithmName object.
            RSAWrapper rsaAlgorithm = new RSAWrapper(DSRsaDropdown.SelectedItem.ToString(), hashAlgorithmName);
            //Create a DigitalSignature object using the dropdown box selection and the RSAWrapper object.
            DigitalSignature digitalSignature = new DigitalSignature(rsaAlgorithm, DSInputTextBox.Text);
            digitalSignature.CreateSignature();
            DSInputTextBlock.Text = "Signature generated successfully!";
        }

        //Authenticate the signature file.
        private void DSAuthenticateButton_Click(object sender, RoutedEventArgs e)
        {
            //Create a DigitalSignature object that will load all the necessary properties from saved files.
            DigitalSignature digitalSignature = new DigitalSignature(DSInputTextBox.Text, DSInputSignatureTextBox.Text, DSInputPublicKeyTextBox.Text);
            DSInputTextBlock.Text = digitalSignature.AuthenticateSignature() ? "Signature valid!" : "Signature invalid!";
        }

        //Select input file.
        private void DCInputButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DCInputTextBox, DCInputTextBlock);
        }

        //Select signature file.
        private void DCInputSignatureButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DCInputSignatureTextBox, DCInputTextBlock);
        }

        //Select envelope file.
        private void DCInputEnvelopeButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DCInputEnvelopeTextBox, DCInputTextBlock);
        }

        //Select public key file.
        private void DCSignaturePublicKeyButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DCInputSignaturePublicKeyTextBox, DCInputTextBlock);
        }

        //Select private key file.
        private void DCEnvelopePrivateKeyButton_Click(object sender, RoutedEventArgs e)
        {
            BasicInputButtonClick(DCInputEnvelopePrivateKeyTextBox, DCInputTextBlock);
        }

        private void DCGenerateCertificateButton_Click(object sender, RoutedEventArgs e)
        {
            //Create a RSAWrapper object using the dropdown box selection.
            RSAWrapper envelopeRsaAlgorithm = new RSAWrapper(DCEnvelopeRsaDropdown.SelectedItem.ToString());
            //Create a CipherMode object using the dropdown box selection.
            CipherMode cipherMode = HelperExtensions.StringToCipherMode(DCEnvelopeCipherDropdown.SelectedItem.ToString());
            //Create a SymmetricAlgorithmWrapper object using the dropdown box selection and the CipherMode object.
            SymmetricAlgorithmWrapper symmetricAlgorithmType = HelperExtensions.StringToSymAlg(DCEnvelopeSymDropdown.SelectedItem.ToString(), cipherMode, DCEnvelopeCipherDropdown.SelectedItem.ToString());

            //Create a DigitalEnvelope object using the dropdown box selection, the SymmetricAlgorithmWrapper object and the RSAWrapper object.
            DigitalEnvelope digitalEnvelope = new DigitalEnvelope(symmetricAlgorithmType, envelopeRsaAlgorithm, DCInputTextBox.Text);
            //Decrypt the envelope.
            DigitalEnvelopeEncrypted result = digitalEnvelope.CreateEnvelope();

            //Create a HashAlgorithmName object using the dropdown box selection.
            HashAlgorithmName hashAlgorithmName = HelperExtensions.StringToHashAlgorithm(DCSignatureHashDropdown.SelectedItem.ToString());
            //Create a RSAWrapper object using the dropdown box selection and the HashAlgorithmName object.
            RSAWrapper rsaAlgorithm = new RSAWrapper(DCSignatureRsaDropdown.SelectedItem.ToString(), hashAlgorithmName);
            //Create a DigitalSignature object using the RSAWrapper object and the encrypted envelope data.
            DigitalSignature digitalSignature = new DigitalSignature(rsaAlgorithm, result.EncryptedDataKeyPair());
            //Create a signature for the encrypted envelope data.
            digitalSignature.CreateSignature();
            DCInputTextBlock.Text = "Certificate generated successfully!";
        }

        private void DCAuthenticateCertificateButton_Click(object sender, RoutedEventArgs e)
        {
            //Create a DigitalEnvelope object that will load all the necessary properties from saved files.
            DigitalEnvelope digitalEnvelope = new DigitalEnvelope(DCInputEnvelopeTextBox.Text, DCInputEnvelopePrivateKeyTextBox.Text);
            //Create a DigitalSignature object that will load all the necessary properties from saved files and the DigitalEnvelope object.
            DigitalSignature digitalSignature = new DigitalSignature(digitalEnvelope.DigitalEnvelopeEncrypted.EncryptedDataKeyPair(), DCInputSignatureTextBox.Text, DCInputSignaturePublicKeyTextBox.Text);

            if (digitalSignature.AuthenticateSignature())
            {
                DCInputTextBlock.Text = "Signature valid!";
            }
            else
            {
                DCInputTextBlock.Text = "Signature invalid!";
                return;
            }
            byte[] result = digitalEnvelope.DecryptEnvelope();
            string decryptedEnvelope = result != null ? Encoding.Unicode.GetString(result) : "Envelope could not be decrypted!";
            DCInputTextBlock.Text += "\n\n-----------------------------------------------\n" + decryptedEnvelope;
        }

        //Open a file dialog box to select a file path.
        //Print selected file path in the textBox and the file content in the textBlock.
        private void BasicInputButtonClick(TextBox textBox, TextBlock textBlock)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox
            // Load content of file in a TextBlock
            if (result == true)
            {
                textBox.Text = openFileDlg.FileName;
                textBlock.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
        }
    }
}
