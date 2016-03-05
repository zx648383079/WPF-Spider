using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Spider.Model
{
    /// <summary>
    /// url 类型
    /// </summary>
    public enum UrlKinds
    {
        /// <summary>
        /// 网页
        /// </summary>
        HTML,
        /// <summary>
        /// js
        /// </summary>
        JS,
        /// <summary>
        /// css
        /// </summary>
        CSS,
        /// <summary>
        /// 文件
        /// </summary>
        FILE
    }

    /// <summary>
    /// 处理方式
    /// </summary>
    public enum DealModes
    {
        /// <summary>
        /// 获取
        /// </summary>
        GET,
        /// <summary>
        /// 下载
        /// </summary>
        DOWNLOAD
    }

    /// <summary>
    /// 一条 url 的信息
    /// </summary>
    public class UrlInformation
    {
        /// <summary>
        /// 名字 包含拓展名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public UrlKinds Kind { get; set; }

        /// <summary>
        /// 下载或获取源码
        /// </summary>
        public DealModes Mode { get; set; }

        /// <summary>
        /// 无参url
        /// </summary>
        public UrlInformation()
        {
            
        }

        /// <summary>
        /// url信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="kind"></param>
        public UrlInformation(string url, UrlKinds kind)
        {
            this.Url = url;
            this.Kind = kind;
        }

    }
}
