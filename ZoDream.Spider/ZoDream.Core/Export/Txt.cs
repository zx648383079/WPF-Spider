using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Core.Export
{
    /// <summary>
    /// 导出txt
    /// </summary>
    public class Txt
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public string Read(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            FileStream stream = new FileStream(path, FileMode.Open);
            string text = Read(stream);
            stream.Close();
            return text;
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string Read(FileStream stream)
        {
            StreamReader reader = new StreamReader(stream, GetEncoding(stream));
            string text = reader.ReadToEnd();
            reader.Close();
            return text;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text"></param>
        public void Write(string path, string text)
        {
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            Write(path, text);
            stream.Close();
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="text"></param>
        public void Write(FileStream stream, string text)
        {
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(text);
            writer.Close();
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Encoding GetEncoding(string path)
        {
            if (!File.Exists(path))
            {
                return Encoding.UTF8;
            }

            FileStream stream = new FileStream(path, FileMode.Open);
            Encoding encoding = GetEncoding(stream);
            return encoding;
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Encoding GetEncoding(FileStream stream)
        {
            Encoding targetEncoding = Encoding.Default;
            if (stream != null && stream.Length >= 2)
            {
                //保存文件流的前4个字节   
                byte byte1 = 0;
                byte byte2 = 0;
                byte byte3 = 0;
                byte byte4 = 0;
                //保存当前Seek位置   
                long origPos = stream.Seek(0, SeekOrigin.Begin);
                stream.Seek(0, SeekOrigin.Begin);

                int nByte = stream.ReadByte();
                byte1 = Convert.ToByte(nByte);
                byte2 = Convert.ToByte(stream.ReadByte());
                if (stream.Length >= 3)
                {
                    byte3 = Convert.ToByte(stream.ReadByte());
                }
                if (stream.Length >= 4)
                {
                    byte4 = Convert.ToByte(stream.ReadByte());
                }
                //根据文件流的前4个字节判断Encoding   
                //Unicode {0xFF, 0xFE};   
                //BE-Unicode {0xFE, 0xFF};   
                //UTF8 = {0xEF, 0xBB, 0xBF};   
                if (byte1 == 0xFE && byte2 == 0xFF)//UnicodeBe   
                {
                    targetEncoding = Encoding.BigEndianUnicode;
                }
                if (byte1 == 0xFF && byte2 == 0xFE && byte3 != 0xFF)//Unicode   
                {
                    targetEncoding = Encoding.Unicode;
                }
                if (byte1 == 0xEF && byte2 == 0xBB && byte3 == 0xBF)//UTF8   
                {
                    targetEncoding = Encoding.UTF8;
                }
                //恢复Seek位置         
                stream.Seek(origPos, SeekOrigin.Begin);
            }
            return targetEncoding;
        }
    }
}
