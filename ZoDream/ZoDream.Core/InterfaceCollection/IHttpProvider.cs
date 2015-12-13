using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoDream.Core.ModelCollection;

namespace ZoDream.Core.InterfaceCollection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpProvider
    {
        /// <summary>
        /// 执行接口
        /// </summary>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        HttpResponseParameter Excute(HttpRequestParameter requestParameter);
    }
}
