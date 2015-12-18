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
        private string _name;
        /// <summary>
        /// 名字 包含拓展名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _url;
        /// <summary>
        /// url
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private UrlKinds _kind;
        /// <summary>
        /// 文件类型
        /// </summary>
        public UrlKinds Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        private DealModes _mode;
        /// <summary>
        /// 下载或获取源码
        /// </summary>
        public DealModes Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

    }
}
