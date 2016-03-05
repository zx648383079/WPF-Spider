using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Spider.Model
{
    /// <summary>
    /// http headers
    /// </summary>
    public class HttpHeader
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 无参初始化
        /// </summary>
        public HttpHeader()
        {
            
        }
        /// <summary>
        /// 传参初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public HttpHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
