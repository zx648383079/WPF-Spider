using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Core.Helper.Url
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class DownLoad
    {
        /// <summary>
        /// Http下载文件
        /// </summary>
        public static bool DownloadFile(string url, string path)
        {
            bool flag = false;
            //打开上次下载的文件
            long offset = 0;
            //实例化流对象
            FileStream stream;
            //判断要下载的文件夹是否存在
            if (File.Exists(path))
            {
                //打开要下载的文件
                stream = File.OpenWrite(path);
                //获取已经下载的长度
                offset = stream.Length;
                stream.Seek(offset, SeekOrigin.Current);
            }
            else
            {
                //文件不保存创建一个文件
                stream = new FileStream(path, FileMode.Create);
                offset = 0;
            }
            try
            {
                //打开网络连接
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                if (offset > 0)
                {
                    webRequest.AddRange((int)offset);             //设置Range值
                }
                //向服务器请求,获得服务器的回应数据流
                Stream responseStream = webRequest.GetResponse().GetResponseStream();
                //定义一个字节数据
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                    if (size < (int)bArr.Length)
                    {
                        bArr = new byte[size];
                    }
                }
                //关闭流
                stream.Close();
                responseStream.Close();
                flag = true;        //返回true下载成功
            }
            catch (Exception)
            {
                stream.Close();
                flag = false;       //返回false下载失败
            }
            return flag;
        }
    }
}
