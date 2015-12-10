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

namespace ZoDream.Core.Helper.Url
{
    /// <summary>
    /// http
    /// </summary>
    public class HttpUtil
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="requestParameter">请求报文</param>
        /// <returns>响应报文</returns>
        public static HttpResponseParameter Excute(HttpRequestParameter requestParameter)
        {
            // 1.实例化
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(requestParameter.Url, UriKind.RelativeOrAbsolute));
            // 2.设置请求头
            SetHeader(webRequest, requestParameter);
            // 3.设置请求Cookie
            SetCookie(webRequest, requestParameter);
            // 4.ssl/https请求设置
            if (Regex.IsMatch(requestParameter.Url, "^https://"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            }
            // 5.设置请求参数[Post方式下]
            SetParameter(webRequest, requestParameter);
            // 6.返回响应报文
            return SetResponse(webRequest, requestParameter);
        }

        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        static void SetHeader(HttpWebRequest webRequest, HttpRequestParameter requestParameter)
        {
            webRequest.Method = requestParameter.IsPost ? "POST" : "GET";
            if (requestParameter.IsPost)
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";
            }
            webRequest.Accept = Accepts.Html;
            webRequest.KeepAlive = true;
            webRequest.UserAgent = UserAgents.Firefox;
            webRequest.AllowAutoRedirect = true;
            webRequest.ProtocolVersion = HttpVersion.Version11;
            if (!string.IsNullOrEmpty(requestParameter.RefererUrl))
            {
                webRequest.Referer = requestParameter.RefererUrl;
            }
        }

        /// <summary>
        /// 设置请求Cookie
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        private static void SetCookie(HttpWebRequest webRequest, HttpRequestParameter requestParameter)
        {
            // 必须实例化，否则响应中获取不到Cookie
            webRequest.CookieContainer = new CookieContainer();
            if (requestParameter.Cookie != null && !string.IsNullOrEmpty(requestParameter.Cookie.CookieString))
            {
                webRequest.Headers[HttpRequestHeader.Cookie] = requestParameter.Cookie.CookieString;
            }
            if (requestParameter.Cookie != null && requestParameter.Cookie.CookieCollection != null && requestParameter.Cookie.CookieCollection.Count > 0)
            {
                webRequest.CookieContainer.Add(requestParameter.Cookie.CookieCollection);
            }
        }

        /// <summary>
        /// ssl/https请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 设置请求参数（只有Post请求方式才设置）
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        static void SetParameter(HttpWebRequest webRequest, HttpRequestParameter requestParameter)
        {
            if (requestParameter.Parameters == null || requestParameter.Parameters.Count <= 0) return;


            if (requestParameter.IsPost)
            {
                StringBuilder data = new StringBuilder(string.Empty);
                foreach (KeyValuePair<string, string> keyValuePair in requestParameter.Parameters)
                {
                    data.AppendFormat("{0}={1}&", keyValuePair.Key, keyValuePair.Value);
                }
                string para = data.Remove(data.Length - 1, 1).ToString();

                byte[] bytePosts = requestParameter.Encoding.GetBytes(para);
                webRequest.ContentLength = bytePosts.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(bytePosts, 0, bytePosts.Length);
                    requestStream.Close();
                }
            }
        }

        /// <summary>
        /// 返回响应报文
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        /// <returns>响应对象</returns>
        static HttpResponseParameter SetResponse(HttpWebRequest webRequest, HttpRequestParameter requestParameter)
        {
            HttpResponseParameter responseParameter = new HttpResponseParameter();
            using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                responseParameter.Uri = webResponse.ResponseUri;
                responseParameter.StatusCode = webResponse.StatusCode;
                responseParameter.Cookie = new HttpCookieType
                {
                    CookieCollection = webResponse.Cookies,
                    CookieString = webResponse.Headers["Set-Cookie"]
                };
                #region 判断解压
                if (webResponse.StatusCode == HttpStatusCode.OK && webResponse.ContentLength < 1024 * 1024)
                {
                    Stream stream = null;
                    if (webResponse.ContentEncoding != null && webResponse.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        stream = new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                    }

                    else
                    {
                        stream = webResponse.GetResponseStream();
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
                    responseParameter.Body = GetEncoding(bytes, webResponse.CharacterSet).GetString(bytes);
                    stream.Close();
                }
                #endregion

                /*using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), requestParameter.Encoding))
                {
                    responseParameter.Body = reader.ReadToEnd();
                }*/
            }
            return responseParameter;
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
