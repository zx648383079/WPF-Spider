using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Core.Import
{
    /// <summary>
    /// 文件的类型
    /// </summary>
    public enum FileKind
    {
        /// <summary>
        /// 网页
        /// </summary>
        Html,
        /// <summary>
        /// js
        /// </summary>
        Js,
        /// <summary>
        /// css
        /// </summary>
        Css,
        /// <summary>
        /// 图片
        /// </summary>
        Image,
        /// <summary>
        /// 视频
        /// </summary>
        Video,
        /// <summary>
        /// 音频文件
        /// </summary>
        Audio,
        /// <summary>
        /// 文件
        /// </summary>
        File
    }
}
