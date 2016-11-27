using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Helper.Security
{
    public class Des3Helper:ISecurityInterface  
    {


        //构造一个对称算法
        private readonly SymmetricAlgorithm _provider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">密钥，必须32位</param>
        /// <param name="iv">向量，必须是12个字符</param>
        public Des3Helper(string key, string iv)
        {
            _provider = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                IV = Encoding.UTF8.GetBytes(iv),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

        }

        #region 加密解密函数

        public string Decrypt(string content)
        {
            var ct = _provider.CreateDecryptor();
            var byt = Convert.FromBase64String(content);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// 字符串的加密
        /// </summary>
        /// <param name="content">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string Encrypt(string content)
        {
            var ct = _provider.CreateEncryptor();
            var byt = Encoding.UTF8.GetBytes(content);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        #endregion
    }
}
