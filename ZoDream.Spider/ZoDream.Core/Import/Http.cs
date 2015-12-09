using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZoDream.Core.Import
{
    /// <summary>
    /// http
    /// </summary>
    public class Http
    {
        private FileInfo _fileInfo;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info"></param>
        public Http(FileInfo info)
        {
            _fileInfo = info;
        }

        /// <summary>
        /// 设置超时时间
        /// </summary>
        public static int Timeout = 10000;

        /// <summary>
        /// cookies
        /// </summary>
        public static CookieCollection Cookies = null;

        /// <summary>
        /// 接受
        /// </summary>
        public static string Accept = Accepts.Html;

        /// <summary>
        /// 代理
        /// </summary>
        public static string UserAgent = UserAgents.Firefox;
        /// <summary>
        /// 请求
        /// </summary>
        public static HttpWebRequest Request = null;
        /// <summary>
        /// 响应
        /// </summary>
        public static HttpWebResponse Response = null;

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public HttpWebResponse Get(FileInfo info)
        {
            _fileInfo = info;
            return Get();
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public HttpWebResponse Get()
        {
            Request = WebRequest.Create(_fileInfo.Url) as HttpWebRequest;
            Request.Method = "GET";
            Request.AllowAutoRedirect = false;
            return _common();
        }

        private HttpWebResponse _common()
        {
            Request.Accept = Accept;
            Request.UserAgent = UserAgent;
            Request.Timeout = Timeout;
            if (!string.IsNullOrEmpty(_fileInfo.Referer))
            {
                Request.Referer = _fileInfo.Referer;
            }
            if (Cookies != null)
            {
                Request.CookieContainer = new CookieContainer();
                Request.CookieContainer.Add(Cookies);
            }
            Response = Request.GetResponse() as HttpWebResponse;
            Cookies = Response.Cookies;
            Request = null;
            
            return Response;
        }


        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="encoding">发送HTTP请求时所用的编码</param>  
        /// <returns></returns>  
        public HttpWebResponse Post(IDictionary<string, string> parameters, Encoding encoding)
        {
            //如果是发送HTTPS请求  
            if (_fileInfo.Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                Request = WebRequest.Create(_fileInfo.Url) as HttpWebRequest;
                Request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                Request = WebRequest.Create(_fileInfo.Url) as HttpWebRequest;
            }
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = encoding.GetBytes(buffer.ToString());
                using (Stream stream = Request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return _common();
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        /// <summary>
        /// Http下载文件
        /// </summary>
        public static string DownloadFile(string url, string path)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();

            //创建本地文件写入流
            Stream stream = new FileStream(path, FileMode.Create);

            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
            return path;
        }

        /// <summary>
        /// 下载html
        /// </summary>
        /// <returns></returns>
        public static string DownloadHtml()
        {
            Stream stream = null;
            string html = "";
            if (Response.StatusCode == HttpStatusCode.OK && Response.ContentLength < 1024 * 1024)
            {
                if (Response.ContentEncoding != null && Response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                {
                    stream = new GZipStream(Response.GetResponseStream(), CompressionMode.Decompress);
                }

                else
                {
                    stream = Response.GetResponseStream();
                }
                #region 把网络流转成内存流
                MemoryStream ms = new MemoryStream();
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int sz = stream.Read(buffer, 0, 1024);
                    if (sz == 0) break;
                    ms.Write(buffer, 0, sz);
                }
                #endregion

                byte[] bytes = ms.ToArray();

                html= GetEncoding(bytes, Response.CharacterSet).GetString(bytes);
                stream.Close();
            }
            return html;
        }

        /// <summary>
        /// 获取HTML网页的编码
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="charSet"></param>
        /// <returns></returns>
        private static Encoding GetEncoding(byte[] bytes, string charSet)
        {
            string html = Encoding.Default.GetString(bytes);
            Regex reg_charset = new Regex(@"charset\b\s*=\s*(?<charset>[^""]*)");
            if (reg_charset.IsMatch(html))
            {

                return Encoding.GetEncoding(reg_charset.Match(html).Groups["charset"].Value);
            }
            else if (charSet != String.Empty)
            {

                return Encoding.GetEncoding(charSet);
            }
            else
                return Encoding.Default;
        }
    }
}
