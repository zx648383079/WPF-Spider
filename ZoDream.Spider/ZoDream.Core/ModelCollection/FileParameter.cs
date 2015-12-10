using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZoDream.Core.EnumCollection;

namespace ZoDream.Core.ModelCollection
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileParameter
    {
        private int _id;
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _url;
        /// <summary>
        /// 完整网址
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private int _depth;
        /// <summary>
        /// 目前所处深度
        /// </summary>
        public int Depth
        {
            get { return _depth; }
            set { _depth = value; }
        }


        private string _path;
        /// <summary>
        /// 保存路径
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private string _referer;
        /// <summary>
        /// 上一个网址
        /// </summary>
        public string Referer
        {
            get { return _referer; }
            set { _referer = value; }
        }

        private FileKind _kind;
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileKind Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }
        

        private HttpStatus _status = HttpStatus.NONE;
        /// <summary>
        /// 下载状态
        /// </summary>
        public HttpStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 得到完整网址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static string GetUrl(string url, string baseUrl)
        {
            return new Uri(new Uri(baseUrl), url).ToString();
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="root">文件路径</param>
        /// <param name="ext">拓展名</param>
        /// <returns></returns>
        public static string GetPath(string name, string root, string ext = "html")
        {
            if (!System.IO.Directory.Exists(root))
            {
                root = System.IO.Path.GetDirectoryName(root);
            }
            return string.Format("{0}\\{1}.{2}", root, name, ext);
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="root">文件路径</param>
        /// <param name="kind">拓展名</param>
        /// <returns></returns>
        public static string GetPath(string url, string root, FileKind kind)
        {
            if (!System.IO.Directory.Exists(root))
            {
                root = System.IO.Path.GetDirectoryName(root);
            }
            if (kind == FileKind.Html)
            {
                return GetPath(GetName(url), root);
            }
            string name = Regex.Match(url, @"\w+?://[\w\.]+?/(?<name>[^\?]+?)\??").Groups["name"].Value;
            string[] filter = { "/", "\\", ":", "|", "*", "?", "<", ">" };
            foreach (string item in filter)
            {
                name = name.Replace(item, "-");
            }
            return string.Format("{0}\\{1}", root + "\\asset", name);
        }

        /// <summary>
        /// 获取名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetName(string url)
        {
            url = Regex.Replace(url, @"[httpHTTPFTPftpsS]{3,5}://[^/]+/", "");
            if (url == "")
            {
                return "index";
            }
            string[] filter = {"/", "\\", ":", "|", "*", "?", "<", ">" };
            foreach (string item in filter)
            {
                url = url.Replace(item, "-");
            }
            return url;
        }

        /// <summary>
        /// 获取文件格式
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static FileKind GetKind(string url)
        {
            string kind = Regex.Match(url, @"\.(?<kind>[\w]+)\?*", RegexOptions.RightToLeft).Groups["kind"].Value.ToLower();
            if (string.IsNullOrWhiteSpace(kind))
            {
                return FileKind.Html;
            }

            switch (kind)
            {
                case "js":
                    return FileKind.Js;
                case "css":
                    return FileKind.Css;
                default:
                    break;
            }
            string pattern = "html|php|asp|aspx|jsp|htm";
            if (Compare(kind, pattern))
            {
                return FileKind.Html;
            }

            pattern = "bmp|jpg|tiff|gif|pcx|tga|exif|fpx|svg|psd|cdr|pcd|dxf|ufo|eps|ai|raw|png|jpeg";
            if (Compare(kind, pattern))
            {
                return FileKind.Image;
            }

            pattern = "mp4|avi|3gp|rmvb|wmv|mkv|mpg|vob|mov|flv";
            if (Compare(kind, pattern))
            {
                return FileKind.Video;
            }
            pattern = "mp3|aac|flac|ape|ogg|mid|wma|wav|cda";
            if (Compare(kind, pattern))
            {
                return FileKind.Audio;
            }
            if (Regex.IsMatch(url, @"\.[\w]+\?"))
            {
                return FileKind.File;
            }
            return FileKind.Html;
        }

        /// <summary>
        /// 判断是否属于
        /// </summary>
        /// <param name="input">要判断的值</param>
        /// <param name="pattern">判断的规则 用 “|” 连接</param>
        /// <returns></returns>
        private static bool Compare(string input, string pattern)
        {
            string[] patterns = pattern.Split('|');
            foreach (string item in patterns)
            {
                if (input == item) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public FileParameter()
        {

        }

        /// <summary>
        /// 初始化化
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="depth"></param>
        /// <param name="path"></param>
        /// <param name="referer"></param>
        /// <param name="kind"></param>
        public FileParameter(int id, string url, string path, int depth = 0, string name = null, string referer = null, FileKind? kind = null)
        {
            this.Id = id;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = Regex.Match(url, @"/(?<name>[^/\.\?]+)[\.\?]*", RegexOptions.RightToLeft).Groups["name"].Value;
            }
            this.Name = name;
            if (referer != null)
            {
                url = GetUrl(url, referer);
            }
            this.Url = url;
            this.Depth = depth;
            this.Path = path;
            this.Referer = referer;
            if (kind == null)
            {
                kind = GetKind(url);
            }
            this.Kind = (FileKind)kind;
        }

    }
}
