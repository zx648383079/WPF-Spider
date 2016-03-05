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
using ZoDream.Core.EnumCollection;
using ZoDream.Core.ModelCollection;
using ZoDream.Spider.Model;

namespace ZoDream.Spider.Helper
{
    /// <summary>
    /// http 处理
    /// </summary>
    public class Http
    {
        private UrlInformation _urlInfo;

        public UrlInformation UrlInfo
        {
            get { return _urlInfo; }
            set { _urlInfo = value; }
        }


        public Http()
        {

        }

        public Http(string url)
        {
            UrlInfo = new UrlInformation(url, UrlKinds.HTML);
        }

        public Http(UrlInformation urlInfo)
        {
            UrlInfo = urlInfo;
        }

        public WebRequest Request()
        {
            WebRequest request = WebRequest.Create(UrlInfo.Url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            request.Timeout = 10000;
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3");
            headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
            request.Headers = headers;
            _setHeader((HttpWebRequest)request);
            if (Regex.IsMatch(UrlInfo.Url, "^https://"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            }
            return request;
        }

        /// <summary>
        /// ssl/https请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="request">HttpWebRequest对象</param>
        private void _setHeader(HttpWebRequest request)
        {
            request.Accept = Accepts.Html;
            request.KeepAlive = true;
            request.UserAgent = UserAgents.Firefox;
            request.Referer = "";
            request.AllowAutoRedirect = true;
            request.ProtocolVersion = HttpVersion.Version11;
            _setCookie(request, new HttpCookieType());
        }

        /// <summary>
        /// 设置请求Cookie
        /// </summary>
        /// <param name="request">HttpWebRequest对象</param>
        /// <param name="cookies">cookies</param>
        private void _setCookie(HttpWebRequest request, HttpCookieType cookies)
        {
            // 必须实例化，否则响应中获取不到Cookie
            request.CookieContainer = new CookieContainer();
            if (cookies != null && !string.IsNullOrEmpty(cookies.CookieString))
            {
                request.Headers[HttpRequestHeader.Cookie] = cookies.CookieString;
            }
            if (cookies != null && cookies.CookieCollection != null && cookies.CookieCollection.Count > 0)
            {
                request.CookieContainer.Add(cookies.CookieCollection);
            }
        }

        private void _post(WebRequest request, IDictionary<string, string> param)
        {
            StringBuilder data = new StringBuilder(string.Empty);
            foreach (KeyValuePair<string, string> keyValuePair in param)
            {
                data.AppendFormat("{0}={1}&", keyValuePair.Key, keyValuePair.Value);
            }
            _post(request, data.Remove(data.Length - 1, 1).ToString());
        }


        private void _post(WebRequest request, string args)
        {
            _post(request, Encoding.UTF8.GetBytes(args));
        }

        private void _post(WebRequest request, byte[] args)
        {
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = args.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(args, 0, args.Length);
                stream.Close();
            }
        }

        /// <summary>
        /// 返回响应报文
        /// </summary>
        /// <param name="request">WebRequest对象</param>
        /// <returns>响应对象</returns>
        public string Response(WebRequest request)
        {
            string html = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                /*responseParameter.Uri = webResponse.ResponseUri;
                responseParameter.StatusCode = webResponse.StatusCode;
                responseParameter.Cookie = new HttpCookieType
                {
                    CookieCollection = webResponse.Cookies,
                    CookieString = webResponse.Headers["Set-Cookie"]
                };*/

                #region 判断解压

                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = null;
                    if (((HttpWebResponse)response).ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        stream = new GZipStream(response.GetResponseStream(), mode: CompressionMode.Decompress);
                    }

                    else
                    {
                        stream = response.GetResponseStream();
                    }
                    #region 把网络流转成内存流
                    MemoryStream ms = new MemoryStream();
                    byte[] buffer = new byte[1024];

                    while (true)
                    {
                        if (stream != null)
                        {
                            int sz = stream.Read(buffer, 0, 1024);
                            if (sz == 0) break;
                            ms.Write(buffer, 0, sz);
                        }
                    }
                    #endregion

                    byte[] bytes = ms.ToArray();
                    html = GetEncoding(bytes, ((HttpWebResponse)response).CharacterSet).GetString(bytes);
                    stream.Close();
                }
                #endregion

                /*using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), requestParameter.Encoding))
                {
                    responseParameter.Body = reader.ReadToEnd();
                }*/
            }
            return html;
        }

        // <summary>
        /// 获取HTML网页的编码
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="charSet"></param>
        /// <returns></returns>
        static Encoding GetEncoding(byte[] bytes, string charSet)
        {
            string html = Encoding.Default.GetString(bytes);
            Regex regCharset = new Regex(@"charset\b\s*=\s*""*(?<charset>[^""]*)");
            if (regCharset.IsMatch(html))
            {
                return Encoding.GetEncoding(regCharset.Match(html).Groups["charset"].Value);
            }

            if (charSet != String.Empty)
            {

                return Encoding.GetEncoding(charSet);
            }

            return Encoding.Default;
        }
    }
}
