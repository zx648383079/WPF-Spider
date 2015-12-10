using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoDream.Core.EnumCollection;
using ZoDream.Core.Helper.Data;
using ZoDream.Core.Helper.FileAndDirectory;
using ZoDream.Core.Helper.Url;
using ZoDream.Core.ModelCollection;

namespace ZoDream.Core.ActionCollection
{
    public class MainAction
    {
        /// <summary>
        /// 程序开始
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="path">文件夹路径</param>
        public static HttpStatus Begin(string url, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string name = FileParameter.GetName(url);
            FileParameter arg = new FileParameter(1, url, FileParameter.GetPath(name, path), 0, name, null, FileKind.Html);
            Begin(ref arg);
            return arg.Status;
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static List<FileParameter> Begin(ref FileParameter args)
        {
            if (!Validator.IsValidUrl(args.Url))
            {
                args.Status = HttpStatus.FAILED;
                return null;
            }
            if (args.Kind == FileKind.Html || args.Kind == FileKind.Css)
            {
                string html = HttpUtil.Excute(new HttpRequestParameter(args.Url, args.Referer)).Body;
                List<FileParameter> template = Html.GetUrl(html, args);
                Txt.Write(args.Path, html);
                return template;
            }
            DownLoad.DownloadFile(args.Url, args.Path);
            return null;
        }
    }
}
