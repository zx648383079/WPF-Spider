using System;
using System.Security.Cryptography;
using System.Text;

namespace ZoDream.Helper.Security
{
    public class AesHelper:ISecurityInterface
    {

        private readonly RijndaelManaged _rijndaelCipher;

        public AesHelper(string password, string iv)
        {
            _rijndaelCipher = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128
            };

            var pwdBytes = Encoding.UTF8.GetBytes(password);

            var keyBytes = new byte[16];

            var len = pwdBytes.Length;

            if (len > keyBytes.Length) len = keyBytes.Length;

            Array.Copy(pwdBytes, keyBytes, len);

            _rijndaelCipher.Key = keyBytes;


            var ivBytes = Encoding.UTF8.GetBytes(iv);
            _rijndaelCipher.IV = ivBytes;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Decrypt(string content)
        {
            var encryptedData = Convert.FromBase64String(content);

            var transform = _rijndaelCipher.CreateDecryptor();

            var plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);
        }

        /// <summary>
        /// 有密码的AES加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encrypt(string content)
        {
            var transform = _rijndaelCipher.CreateEncryptor();

            var plainText = Encoding.UTF8.GetBytes(content);

            var cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);

            return Convert.ToBase64String(cipherBytes);
        }
        

        /// <summary>
        /// 随机生成密钥
        /// </summary>
        /// <returns></returns>
        public static string GetIv(int n)
        {
            var arrChar = new[]{
           'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',
           '0','1','2','3','4','5','6','7','8','9',
           'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
          };

            var num = new StringBuilder();

            var rnd = new Random(DateTime.Now.Millisecond);
            for (var i = 0; i < n; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());

            }

            return num.ToString();
        }
    }
}
