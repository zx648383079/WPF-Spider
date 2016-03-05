using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZoDream.ToolBox.Model
{
    /// <summary>
    /// 菜单的信息
    /// </summary>
    public class ToolInformation
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// 名字 可以重命名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 单个工具信息 （待拓展，暂时只支持程序，未支持dll）
        /// </summary>
        public ToolInformation()
        {
            
        }

        /// <summary>
        /// 单个工具信息
        /// </summary>
        /// <param name="path">路径</param>
        public ToolInformation(string path)
        {
            this.Path = path;
            this.Name = Regex.Match(path, @"[\\/](?<name>[^\.]+)", RegexOptions.RightToLeft).Groups["name"].Value;
        }
    }
}
