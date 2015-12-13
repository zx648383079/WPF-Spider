using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Core.ModelCollection
{
    /// <summary>
    /// 请求参数类
    /// </summary>
    public class HttpRequestParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpRequestParameter()
        {
            Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="referer"></param>
        /// <param name="cookie"></param>
        public HttpRequestParameter(string url, string referer = null, HttpCookieType cookie = null)
        {
            Url = url;
            IsPost = false;
            Parameters = null;
            Encoding = Encoding.UTF8;
            RefererUrl = referer;
            Cookie = cookie;
        }

        /// <summary>
        /// post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isPost"></param>
        /// <param name="param"></param>
        /// <param name="encoding"></param>
        /// <param name="referer"></param>
        /// <param name="cookie"></param>
        public HttpRequestParameter(string url, bool isPost = false, IDictionary<string, string> param = null, Encoding encoding = null, string referer = null, HttpCookieType cookie = null)
        {
            Url = url;
            IsPost = isPost;
            Parameters = param;
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            Encoding = encoding;
            RefererUrl = referer;
            Cookie = cookie;
        }
        /// <summary>
        /// 请求方式：true表示post,false表示get
        /// </summary>
        public bool IsPost { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 请求Cookie对象
        /// </summary>
        public HttpCookieType Cookie { get; set; }
        /// <summary>
        /// 请求编码
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public IDictionary<string, string> Parameters { get; set; }
        /// <summary>
        /// 引用页
        /// </summary>
        public string RefererUrl { get; set; }
    }
}
