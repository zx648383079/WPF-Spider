using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Server.Model
{
    /// <summary>
    /// 请求值
    /// </summary>
    public class HttpValue
    {
        /// <summary>
        /// 名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HttpValue()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public HttpValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
