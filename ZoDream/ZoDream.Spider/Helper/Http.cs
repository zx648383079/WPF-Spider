using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoDream.Spider.Model;

namespace ZoDream.Spider.Helper
{
    /// <summary>
    /// http 处理
    /// </summary>
    public class Http
    {
        private UrlInformation _urlInfo;

        public Http()
        {

        }

        public Http(UrlInformation urlInfo)
        {
            _urlInfo = urlInfo;
        }

        public object Get(UrlInformation urlInfo)
        {
            _urlInfo = urlInfo;

            return null;
        }

        public object Get()
        {

            return null;
        }
    }
}
