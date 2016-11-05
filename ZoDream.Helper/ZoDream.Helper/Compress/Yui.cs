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
            CssCompressor compressor = new CssCompressor {RemoveComments = removeComments};
            return compressor.Compress(content);
        }
    }
}
