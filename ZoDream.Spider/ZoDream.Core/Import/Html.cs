using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZoDream.Core.Import
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
        /// <param name="referer"></param>
        /// <param name="path"></param>
        /// <param name="depth">深度</param>
        /// <param name="start">起始编号</param>
        /// <returns></returns>
        public List<FileInfo> GetUrl(string html, string referer, string path, int depth = 0, int start = 0)
        {
            List<FileInfo> fileInfos = new List<FileInfo>();
            MatchCollection ms = Regex.Matches(html, @"\<\s*(?<tag>[^\s\<\>]+)[^\<\>]+[hrefHREFscSC]{3,4}\s*=\s*[""']*(?<url>[^\s""'\>\<]+)\s *[""']*");
            foreach (Match item in ms)
            {
                FileKind? kind = null;
                switch (item.Groups["tag"].Value.ToLower())
                {
                    case "a":
                        break;
                    case "link":
                        kind = FileKind.Css;
                        path += "/asset";
                        break;
                    case "script":
                        kind = FileKind.Js;
                        path += "/asset";
                        break;
                    case "img":
                        kind = FileKind.Image;
                        path += "/asset";
                        break;
                    case "audio":
                        kind = FileKind.Audio;
                        path += "/asset";
                        break;
                    case "video":
                        kind = FileKind.Video;
                        path += "/asset";
                        break;
                    default:
                        break;
                }
                string url = item.Groups["url"].Value;

                //item.Value.Replace(url, )

                fileInfos.Add(new FileInfo(start++, url, path, depth, null, referer, kind));

            }
            return fileInfos;
        }

        public string Filter(string html, List<PatternCollection> pattern)
        {
            foreach (PatternCollection item in pattern)
            {
                Regex regex = new Regex(item.Pattern);
                switch (item.Kind)
                {
                    case PatternKind.MATCH:
                        Match m = regex.Match(html);
                        if (string.IsNullOrWhiteSpace(item.Value))
                        {
                            html = m.Value;
                        } else
                        {
                            html = m.Groups[item.Value].Value;
                        }
                        break;
                    case PatternKind.REPLACE:
                        html = regex.Replace(html, item.Value);
                        break;
                    default:
                        break;
                }
            }

            return html;
        }
        
    }
}
