using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Core.Import
{
    /// <summary>
    /// 匹配的信息
    /// </summary>
    public class PatternCollection
    {
        private string _pattern;
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        private string _value;
        /// <summary>
        /// 要获取的值或要替换的值
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private PatternKind _kind;
        /// <summary>
        /// 匹配方式
        /// </summary>
        public PatternKind Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }
        ///
    }
}
