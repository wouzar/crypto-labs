using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace DESEncryption
{
    public partial class Main : Form
    {

        DESCryptoServiceProvider cryptic = null;
        private static byte[] DESKey = { 0, 3, 5, 24, 53, 23, 4, 80 };
        private static byte[] DESInitializationVector = { 0 , 43, 23, 53, 2, 53, 2 ,54 };


        public Main()
        {
            InitializeComponent();

            cryptic = new DESCryptoServiceProvider();
            cryptic.Key = ASCIIEncoding.ASCII.GetBytes("DefaultK");
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes("DefaultV");
            cryptic.Mode = CipherMode.CBC;
    
        }

        public static string Encrypt(string value)
        {
            using (var cryptoProvider = new DESCryptoServiceProvider())
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(DESKey, DESInitializationVector), CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cryptoStream))
            {
                writer.Write(value);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();
                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
        }

        public static string Decrypt(string value)
        {
            using (var cryptoProvider = new DESCryptoServiceProvider())
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            using (var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(DESKey, DESInitializationVector), CryptoStreamMode.Read))
            using (var reader = new StreamReader(cryptoStream))
            {
                return reader.ReadToEnd();
            }
        }

        private string encodeString(string str)
        {
            byte[] data = ASCIIEncoding.ASCII.GetBytes(inputText.Text);
            MemoryStream stream = new MemoryStream(data);
            CryptoStream crStream = new CryptoStream(stream, cryptic.CreateEncryptor(), CryptoStreamMode.Read);

            StreamReader reader = new StreamReader(crStream);
            return reader.ReadToEnd();
        }

        private string decodeString(string str)
        {
            byte[] data = ASCIIEncoding.ASCII.GetBytes(inputText.Text);
            MemoryStream stream = new MemoryStream(data);
            CryptoStream crStream = new CryptoStream(stream, cryptic.CreateDecryptor(), CryptoStreamMode.Read);

            StreamReader reader = new StreamReader(crStream);
            return reader.ReadToEnd();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            //FileStream fs = new FileStream(@"d:\test.txt", FileMode.OpenOrCreate, FileAccess.Write);
            outputText.Text = Encrypt(inputText.Text);




            //crStream.Write(data, 0, data.Length);

            //crStream.Close();



            //fs.Close();
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {

            outputText.Text = Decrypt(inputText.Text);
            //cryptic = new DESCryptoServiceProvider();
            //cryptic.Key = ASCIIEncoding.ASCII.GetBytes("Kekasuss");
            //cryptic.IV = ASCIIEncoding.ASCII.GetBytes("Kekasuss");
            //cryptic.Mode = CipherMode.CBC;

            //FileStream fs = new FileStream(@"d:\test.txt", FileMode.Open, FileAccess.Read);

            //CryptoStream crStream = new CryptoStream(fs, cryptic.CreateDecryptor(), CryptoStreamMode.Read);

            //StreamReader reader = new StreamReader(crStream);

            //string data = reader.ReadToEnd();

            //MessageBox.Show(data);

            //reader.Close();

            //fs.Close();

        }
    }
}
