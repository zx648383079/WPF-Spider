using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoDream.Core.Helper.Url;
using ZoDream.Core.InterfaceCollection;
using ZoDream.Core.ModelCollection;

namespace ZoDream.Core.ProviderCollection
{
    /// <summary>
    /// http
    /// </summary>
    public class HttpProvider : IHttpProvider
    {
        /// <summary>
        /// http执行
        /// </summary>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        public HttpResponseParameter Excute(HttpRequestParameter requestParameter)
        {
            return HttpUtil.Excute(requestParameter);
        }
    }
}
