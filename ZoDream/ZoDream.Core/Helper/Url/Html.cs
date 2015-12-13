using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZoDream.Core.EnumCollection;
using ZoDream.Core.ModelCollection;

namespace ZoDream.Core.Helper.Url
{
    /// <summary>
    /// html解析
    /// </summary>
    public class Html
    {
        /// <summary>
        /// 获取url列表
        /// </summary>
        /// <param name="html"></param>
        /// <param name="arg"></param>
        /// <param name="start">起始编号</param>
        /// <returns></returns>
        public static List<FileParameter> GetUrl(string html, FileParameter arg, int start = 0)
        {
            switch (arg.Kind)
            {
                case FileKind.Html:
                    return HtmlUrl(html, arg, start);
                case FileKind.Js:
                    break;
                case FileKind.Css:
                    return CssUrl(html, arg, 0);
                case FileKind.Image:
                    break;
                case FileKind.Video:
                    break;
                case FileKind.Audio:
                    break;
                case FileKind.File:
                    break;
                case FileKind.Unkown:
                    break;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 获取url列表
        /// </summary>
        /// <param name="html"></param>
        /// <param name="arg"></param>
        /// <param name="start">起始编号</param>
        /// <returns></returns>
        public static List<FileParameter> HtmlUrl(string html, FileParameter arg, int start = 0)
        {
            List<FileParameter> fileInfos = new List<FileParameter>();
            MatchCollection ms = Regex.Matches(html, @"\<\s*(?<tag>[^\s\<\>]+)[^\<\>]+[hrefHREFscSC]{3,4}\s*=\s*[""']*(?<url>[^\s""'\>\<]+)\s*[""']*");
            foreach (Match item in ms)
            {
                FileKind kind;
                string url = FileParameter.GetUrl(item.Groups["url"].Value, arg.Url);
                switch (item.Groups["tag"].Value.ToLower())
                {
                    case "a":
                        kind = FileParameter.GetKind(url);
                        break;
                    case "link":
                        kind = FileKind.Css;
                        break;
                    case "script":
                        kind = FileKind.Js;
                        break;
                    case "img":
                        kind = FileKind.Image;
                        break;
                    case "audio":
                        kind = FileKind.Audio;
                        break;
                    case "video":
                        kind = FileKind.Video;
                        break;
                    default:
                        kind = FileKind.Unkown;
                        break;
                }
                fileInfos.Add(new FileParameter(start ++, url, FileParameter.GetPath(url, arg.Path, kind), arg.Depth + 1, null, arg.Url, kind));

            }
            return fileInfos;
        }

        /// <summary>
        /// 获取url列表
        /// </summary>
        /// <param name="html"></param>
        /// <param name="arg"></param>
        /// <param name="start">起始编号</param>
        /// <returns></returns>
        public static List<FileParameter> CssUrl(string html, FileParameter arg, int start = 0)
        {
            List<FileParameter> fileInfos = new List<FileParameter>();
            MatchCollection ms = Regex.Matches(html, @"url\s?\([""']?(?<url>[^\)""']+)[""']?\s?\)");
            foreach (Match item in ms)
            {
                string url = FileParameter.GetUrl(item.Groups["url"].Value, arg.Url);
                Match m = Regex.Match(url, @"\.[^\./]+?/(?<name>[^\.\s]+)\.(?<ext>[^\.\?]+)");
                string name = m.Groups["name"].Value;
                fileInfos.Add(new FileParameter(start++, url, FileParameter.GetPath(name, arg.Path, m.Groups["ext"].Value), arg.Depth + 1, name, arg.Url, FileKind.Image));
            }
            return fileInfos;
        }
    }
}
