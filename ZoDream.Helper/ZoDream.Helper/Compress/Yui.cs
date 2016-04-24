using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahoo.Yui.Compressor;

namespace ZoDream.Helper.Compress
{
    public class Yui
    {
        public string Js(string content)
        {
            JavaScriptCompressor jscompressor = new JavaScriptCompressor();
            return jscompressor.Compress(content);
        }

        public string Css(string content, bool removeComments = true)
        {
            CssCompressor compressor = new CssCompressor();
            compressor.RemoveComments = removeComments;
            return compressor.Compress(content);
        }
    }
}
