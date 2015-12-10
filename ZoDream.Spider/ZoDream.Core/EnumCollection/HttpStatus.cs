using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Core.EnumCollection
{
    /// <summary>
    /// 下载状态
    /// </summary>
    public enum HttpStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        NONE,
        /// <summary>
        /// 等待中
        /// </summary>
        WAITTING,
        /// <summary>
        /// 用户放弃的
        /// </summary>
        USELESS,
        /// <summary>
        /// 下载中
        /// </summary>
        DOWNLOADING,
        /// <summary>
        /// 完成
        /// </summary>
        COMPLETE,
        /// <summary>
        /// 失败
        /// </summary>
        FAILED
    }
}
