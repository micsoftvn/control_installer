using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Net;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CS2PHPCryptography;
//using PHP_Encryption;


namespace WindowsFormsApp1
{
    public partial class FormMain : Form
    {
        private OpenFileDialog openFileDialog;
        private string ad_pass = "";
        private string ad_user = "quangva_1"; // User có quyền Install trên hệ thống
        private string ad_domain = "micsoftvn.com";  // Domain sử dụng trong hệ thống
        private static readonly byte[] SALT = Encoding.ASCII.GetBytes("micsoftvn!#@".Length.ToString());
		
		//rsa = new RSAtoPHPCryptography();
        //private List<string> verifyData = new List<string>();
        public FormMain()
        {
            InitializeComponent();
			
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
		public static class Utility
    {
        public static string ToUrlSafeBase64(byte[] input)
        {
            return Convert.ToBase64String(input).Replace("+", "-").Replace("/", "_");
        }

        public static byte[] FromUrlSafeBase64(string input)
        {
            return Convert.FromBase64String(input.Replace("-", "+").Replace("_", "/"));
        }
    }
	public class AEStoPHPCryptography
    {
        private byte[] Key;
        private byte[] IV;

        /// <summary>
        /// Gets the encryption key as a base64 encoded string.
        /// </summary>
        public string EncryptionKeyString
        {
            get { return Convert.ToBase64String(Key); }
        }

        /// <summary>
        /// Gets the initialization key as a base64 encoded string.
        /// </summary>
        public string EncryptionIVString
        {
            get { return Convert.ToBase64String(IV); }
        }

        /// <summary>
        /// Gets the encryption key.
        /// </summary>
        public byte[] EncryptionKey
        {
            get { return Key; }
        }

        /// <summary>
        /// Gets the initialization key.
        /// </summary>
        public byte[] EncryptionIV
        {
            get { return IV; }
        }

        public AEStoPHPCryptography()
        {
            Key = new byte[256 / 8];
            IV = new byte[128 / 8];

            GenerateRandomKeys();
        }

        public AEStoPHPCryptography(string key, string iv)
        {
            Key = Convert.FromBase64String(key);
            IV = Convert.FromBase64String(iv);

            if (Key.Length * 8 != 256)
                throw new Exception("The Key must be exactally 256 bits long!");
            if (IV.Length * 8 != 128)
                throw new Exception("The IV must be exactally 128 bits long!");
        }

        /// <summary>
        /// Generate the cryptographically secure random 256 bit Key and 128 bit IV for the AES algorithm.
        /// </summary>
        public void GenerateRandomKeys()
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            random.GetBytes(Key);
            random.GetBytes(IV);
        }

        /// <summary>
        /// Encrypt a message and get the encrypted message in a URL safe form of base64.
        /// </summary>
        /// <param name="plainText">The message to encrypt.</param>
        public string Encrypt(string plainText)
        {
            return Utility.ToUrlSafeBase64(Encrypt2(plainText));
        }

        /// <summary>
        /// Encrypt a message using AES.
        /// </summary>
        /// <param name="plainText">The message to encrypt.</param>
        private byte[] Encrypt2(string plainText)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                StreamWriter swEncrypt = new StreamWriter(csEncrypt);

                swEncrypt.Write(plainText);

                swEncrypt.Close();
                csEncrypt.Close();
                aes.Clear();

                return msEncrypt.ToArray();
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Problem trying to encrypt.", ex);
            }
        }

        /// <summary>
        /// Decrypt a message that is in a url safe base64 encoded string.
        /// </summary>
        /// <param name="cipherText">The string to decrypt.</param>
        public string Decrypt(string cipherText)
        {
            return Decrypt2(Utility.FromUrlSafeBase64(cipherText));
        }

        /// <summary>
        /// Decrypt a message that was AES encrypted.
        /// </summary>
        /// <param name="cipherText">The string to decrypt.</param>
        private string Decrypt2(byte[] cipherText)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                MemoryStream msDecrypt = new MemoryStream(cipherText);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                StreamReader srDecrypt = new StreamReader(csDecrypt);

                string plaintext = srDecrypt.ReadToEnd();

                srDecrypt.Close();
                csDecrypt.Close();
                msDecrypt.Close();
                aes.Clear();

                return plaintext;
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Problem trying to decrypt.", ex);
            }
        }
    }
	
        // Get MD5 file //
        private static string GetMD5HashFromFile(string fileName)
        {
            FileStream inputStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(inputStream);
            inputStream.Close();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString().ToUpper();
        }
        // Get MD5 file //
        // Get Encrypt file //
        private static string Encrypt(string inputText)
        {
            string str;
            RijndaelManaged managed = new RijndaelManaged();
            byte[] buffer = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes bytes = new PasswordDeriveBytes("v1n3t.C0M!!!", SALT);
            using (ICryptoTransform transform = managed.CreateEncryptor(bytes.GetBytes(0x20), bytes.GetBytes(0x10)))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                    {
                        stream2.Write(buffer, 0, buffer.Length);
                        stream2.FlushFinalBlock();
                        str = Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return str;
        }
        protected string Get(string url)
        {
            try
            {
                string rt;
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                rt = reader.ReadToEnd();
                //Console.WriteLine(rt);
                reader.Close();
                response.Close();
                return rt;
            }

            catch (Exception exception)
            {
                //MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                MessageBox.Show("Lỗi xảy ra kết nối đến máy chủ, vui lòng thử lại sau.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Application.Exit();
                return "Error: " + exception.Message;
                //Application.Exit();

            }
        }
        private static string Decrypt(string inputText)
        {
            string str;
            RijndaelManaged managed = new RijndaelManaged();
            byte[] buffer = Convert.FromBase64String(inputText);
            PasswordDeriveBytes bytes = new PasswordDeriveBytes("v1n3t.C0M!!!", SALT);
            using (ICryptoTransform transform = managed.CreateDecryptor(bytes.GetBytes(0x20), bytes.GetBytes(0x10)))
            {
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                    {
                        byte[] buffer2 = new byte[buffer.Length];
                        int count = stream2.Read(buffer2, 0, buffer2.Length);
                        str = Encoding.Unicode.GetString(buffer2, 0, count);
                    }
                }
            }
            return str;
        }
        // Get Encrypt file //
        private void buttonFile_Click_1(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxFile.Text = this.openFileDialog.FileName;
                try
                {
                    this.textBoxMD5.Text = GetMD5HashFromFile(this.textBoxFile.Text);
                    this.buttonInstall.Enabled = true;
                }
                catch (Exception exception)
                {
                    this.textBoxMD5.Text = string.Empty;
                    this.buttonInstall.Enabled = false;
                    MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
        private static SecureString GetSecureString(string value)
        {
            SecureString str = new SecureString();
            foreach (char ch in value)
            {
                str.AppendChar(ch);
            }
            return str;
        }
        private static void RunAs(string domain, string username, string password, string path)
        {
			try
                {
            new Process
            {
                StartInfo = {
                FileName = "cmd.exe",
				//FileName = "C:\\test\\uiso961pes.exe",
                Arguments = "/C \"" + path + "\"",
                UseShellExecute = false,
                UserName = username,
                Password = GetSecureString(password),
                Domain = domain,
                Verb = "runas",
                LoadUserProfile = true
            }
            }.Start();
				} catch
                {
					MessageBox.Show("Lỗi xác thực xin thông báo về P.ATTT để được hỗ trợ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				    Application.Exit();
                    return;
                }
        }
        private bool VerifySetupFile(string path)
        {
            string extension = Path.GetExtension(path);
            if ((extension != null) && ((extension == ".exe") || (extension == ".msi") || (extension == ".EXE") || (extension == ".MSI")))
            {
                return true;
            }
            MessageBox.Show("File không được hỗ trợ.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
        }
         private void buttonInstall_Click(object sender, EventArgs e)
        {
            try
            {
                //this.GetVerifyData();
				/////////////
				AEStoPHPCryptography aes = new AEStoPHPCryptography("YWJjZGVmZ2hpamtsbW5vcGFiY2RlZmdoaWprbG1ub3A=", "YWJjZGVmZ2hpamtsbW5vcA==");
				RSAtoPHPCryptography rsa;
			rsa = new RSAtoPHPCryptography();
			rsa.LoadCertificateFromString("-----BEGIN CERTIFICATE-----MIID2jCCA0OgAwIBAgIJAPEru6Ch9es0MA0GCSqGSIb3DQEBBQUAMIGlMQswCQYDVQQGEwJVUzEQMA4GA1UECBMHRmxvcmlkYTESMBAGA1UEBxMJUGVuc2Fjb2xhMRswGQYDVQQKExJTY290dCBUZXN0IENvbXBhbnkxGTAXBgNVBAsTEFNlY3VyaXR5IFNlY3Rpb24xFjAUBgNVBAMTDVNjb3R0IENsYXl0b24xIDAeBgkqhkiG9w0BCQEWEXNzbEBzcGFya2hpdHouY29tMB4XDTExMDcwNDEzMDczM1oXDTIxMDcwMTEzMDczNFowgaUxCzAJBgNVBAYTAlVTMRAwDgYDVQQIEwdGbG9yaWRhMRIwEAYDVQQHEwlQZW5zYWNvbGExGzAZBgNVBAoTElNjb3R0IFRlc3QgQ29tcGFueTEZMBcGA1UECxMQU2VjdXJpdHkgU2VjdGlvbjEWMBQGA1UEAxMNU2NvdHQgQ2xheXRvbjEgMB4GCSqGSIb3DQEJARYRc3NsQHNwYXJraGl0ei5jb20wgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAKLEwtnhSD3sUMidycowAhupy59PMh8FYX6ebKy4NYqEiFONzrujkGtAZgmUaCAQBEmGcfBUDVd4ew72Xjikq0WhBUju+wmrIcgnQcIMAXMkZ2gBV12SkvCzRrJf5zqO0rC0x/tBli/46KGrzyYLl7K3QFx3MQPNvVO+w/b0coatAgMBAAGjggEOMIIBCjAdBgNVHQ4EFgQU+6E6OauoEUohJOAgC8OXU3xaHn4wgdoGA1UdIwSB0jCBz4AU+6E6OauoEUohJOAgC8OXU3xaHn6hgaukgagwgaUxCzAJBgNVBAYTAlVTMRAwDgYDVQQIEwdGbG9yaWRhMRIwEAYDVQQHEwlQZW5zYWNvbGExGzAZBgNVBAoTElNjb3R0IFRlc3QgQ29tcGFueTEZMBcGA1UECxMQU2VjdXJpdHkgU2VjdGlvbjEWMBQGA1UEAxMNU2NvdHQgQ2xheXRvbjEgMB4GCSqGSIb3DQEJARYRc3NsQHNwYXJraGl0ei5jb22CCQDxK7ugofXrNDAMBgNVHRMEBTADAQH/MA0GCSqGSIb3DQEBBQUAA4GBAJ8lRVFiLgfxiHsrPvhY+i05FYnDskit9QTnBv2ScM7rfK+EKfOswjxv9sGdGqKaTYE684XCmrwxCx42hNOSgMGDiZAlNoBJdJbF/bw2Qr5HUmZ8G3L3UlB1+qyM0+JkXMqkVcoIR7Ia5AGZHe9/QAwD3nA9rf3diH2LWATtgWNB-----END CERTIFICATE-----");
				///////////// aes/aes.php
				string item = GetMD5HashFromFile(this.textBoxFile.Text);
				string code_item = rsa.Encrypt(item);
                string value_get_md5 = this.Get("http://domain.com/syslog/api_appinstall.php?act=api&app=" + code_item);  // Domain check kiem tra
				//string value_get_md5 = this.Get("http://domain.com/aes/rsa.php?code=" + code_item);
				///////////// Ket thuc lay gia tri //////////////
				
				value_get_md5 = value_get_md5.TrimEnd('\r', '\n');
				string chuoi_mahoa = aes.Decrypt(value_get_md5);
			    string[] chuoi_tach = chuoi_mahoa.Split(new Char[] { ':'});
                //if (this.VerifySetupFile(this.textBoxFile.Text) && "508FF3FBD74DD96E39A274C331C6279F" == item )
					if (this.VerifySetupFile(this.textBoxFile.Text) && value_get_md5 != "" && chuoi_tach[0] == item )
                {
                    RunAs(this.ad_domain, this.ad_user, chuoi_tach[1], this.textBoxFile.Text);
                    //MessageBox.Show(chuoi_tach[1], "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					MessageBox.Show("Ok file của bạn được chấp nhận - Done ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				
                }
				else {
				MessageBox.Show("File của bạn chưa được kiểm duyệt - Liên hệ P.ATTT để được hỗ trợ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Application.Exit();
				}
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void labelInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
