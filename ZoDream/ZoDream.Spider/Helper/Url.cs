using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZoDream.Core.EnumCollection;
using ZoDream.Core.ModelCollection;
using ZoDream.Spider.Model;

namespace ZoDream.Spider.Helper
{
    /// <summary>
    /// url
    /// </summary>
    public class Url
    {
        /// <summary>
        /// 判断是否是URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsUrl(string url)
        {
            Uri uri;
            return Uri.TryCreate(url, UriKind.Absolute, out uri);
        }

        /// <summary>
        /// 获取完整的url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetComplete(string url)
        {
            int index = url.IndexOf("//");
            if (index < 0)
            {
                return "http://" + url;
            }
            if (index == 0)
            {
                return "http:" + url;
            }
            if (url.IndexOf("://") == 0)
            {
                return "http" + url;
            }
            return url;
        }


        /// <summary>
        /// 获取url列表
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<UrlInformation> GetUrlFromHtml(string html)
        {
            List<UrlInformation> urlsinfo = new List<UrlInformation>();
            MatchCollection ms = Regex.Matches(html, @"\<\s*(?<tag>[^\s\<\>]+)[^\<\>]+[hrefHREFscSC]{3,4}\s*=\s*[""']*(?<url>[^\s""'\>\<#]+)\s*[""']*");
            foreach (Match item in ms)
            {
                UrlKinds kind;
                string url = item.Groups["url"].Value;
                if (string.IsNullOrWhiteSpace(url))
                {
                    continue;
                }
                switch (item.Groups["tag"].Value.ToLower())
                {
                    case "a":
                        kind = UrlKinds.HTML;
                        break;
                    case "link":
                        kind = UrlKinds.CSS;
                        break;
                    case "script":
                        kind = UrlKinds.JS;
                        break;
                    case "img":
                        kind = UrlKinds.FILE;
                        break;
                    case "audio":
                        kind = UrlKinds.FILE;
                        break;
                    case "video":
                        kind = UrlKinds.FILE;
                        break;
                    default:
                        kind = UrlKinds.FILE;
                        break;
                }
                urlsinfo.Add(new UrlInformation(url, kind));

            }
            return urlsinfo;
        }
    }
}
