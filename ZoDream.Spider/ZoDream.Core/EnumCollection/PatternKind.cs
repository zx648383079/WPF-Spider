using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Core.EnumCollection
{
    /// <summary>
    /// 正则模式
    /// </summary>
    public enum PatternKind
    {
        /// <summary>
        /// 普通替换
        /// </summary>
        COMMON,
        /// <summary>
        /// 获取
        /// </summary>
        MATCH,
        /// <summary>
        /// 替换
        /// </summary>
        REPLACE
    }
}
