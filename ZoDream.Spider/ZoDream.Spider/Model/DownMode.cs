using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Spider.Model
{
    /// <summary>
    /// 下载方式
    /// </summary>
    public enum DownMode
    {
        /// <summary>
        /// 所有
        /// </summary>
        ALL,
        /// <summary>
        /// 本级及下级
        /// </summary>
        NEXT,
        /// <summary>
        /// 本页及本页所有网址
        /// </summary>
        PAGEANDURL,
        /// <summary>
        /// 本页
        /// </summary>
        PAGE
    }
}
