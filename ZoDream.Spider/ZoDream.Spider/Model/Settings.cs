using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoDream.Core.Import;

namespace ZoDream.Spider.Model
{
    /// <summary>
    /// 全局设置
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// 初始网址
        /// </summary>
        public static string Url = null;

        /// <summary>
        /// 下载方式
        /// </summary>
        public static DownMode Mode = DownMode.ALL;

        /// <summary>
        /// 下载内容
        /// </summary>
        public static Dictionary<FileKind, bool> Kind = new Dictionary<FileKind, bool>();

        /// <summary>
        /// 下载深度
        /// </summary>
        public static int Depth = 0;

        /// <summary>
        /// 保存路径 默认我的文档
        /// </summary>
        public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }
}
