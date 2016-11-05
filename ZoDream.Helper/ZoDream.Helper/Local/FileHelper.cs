using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ZoDream.Helper.Local
{
    public class FileHelper
    {
        public string FileName { get; set; }

        public FileHelper()
        {

        }

        public FileHelper(string file)
        {
            FileName = file;
        }

        public string GetMd5()
        {
            if (!File.Exists(FileName))
            {
                return string.Empty;
            }
            try
            {
                byte[] buffers;
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    using (var fs = new FileStream(FileName, FileMode.Open))
                    {
                        buffers = md5.ComputeHash(fs);
                    }
                }
                var sb = new StringBuilder();
                foreach (var item in buffers)
                {
                    sb.Append(item.ToString("X2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetSha1()
        {
            if (!File.Exists(FileName))
            {
                return string.Empty;
            }
            byte[] buffers;
            using (var hash = new SHA1Managed()) // 创建Hash算法对象
            {
                using (var fs = new FileStream(FileName, FileMode.Open)) // 创建文件流对象
                {
                    buffers = hash.ComputeHash(fs); // 计算   
                }
            }
            var sb = new StringBuilder();
            foreach (var item in buffers)
            {
                sb.Append(item.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
