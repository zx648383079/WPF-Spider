using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZoDream.Controls.Model
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileKind Kind { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public FileInfo()
        {
            
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="kind"></param>
        public FileInfo(string path, FileKind kind)
        {
            Name = Regex.Match(path, @"[^\\]*", RegexOptions.RightToLeft).Value;
            Path = path;
            Kind = kind;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="kind"></param>
        public FileInfo(string name, string path, FileKind kind)
        {
            Name = name;
            Path = path;
            Kind = kind;
        }
    }
}
