using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Helper.Security
{
    public class DesHelper : ISecurityInterface
    {
        //构造一个对称算法
        private readonly SymmetricAlgorithm _provider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">密钥，必须32位</param>
        /// <param name="iv">向量，必须是12个字符</param>
        public DesHelper(string key, string iv)
        {
            _provider = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                IV = Encoding.UTF8.GetBytes(iv)
            };

        }

        public string Decrypt(string content)
        {

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(content);
            }
            catch
            {
                return null;
            }
            var ms = new MemoryStream(byEnc);
            var cst = new CryptoStream(ms, _provider.CreateDecryptor(), CryptoStreamMode.Read);
            var sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        public string Encrypt(string content)
        {
            var ms = new MemoryStream();
            var cst = new CryptoStream(ms, _provider.CreateEncryptor(), CryptoStreamMode.Write);

            var sw = new StreamWriter(cst);
            sw.Write(content);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }
    }
}
