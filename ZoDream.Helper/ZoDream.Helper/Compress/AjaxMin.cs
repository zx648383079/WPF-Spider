using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Helper.Compress
{
    public class AjaxMin
    {
        public string Js(string content, string command = "-comments:none")
        {
            var switchParser = new SwitchParser();
            switchParser.Parse(command);
            var minifier = new Minifier();
            return minifier.MinifyJavaScript(content, switchParser.JSSettings);
        }

        public string Css(string content, string command = "-css:decls -colors:hex -comments:none")
        {
            var switchParser = new SwitchParser();
            switchParser.Parse(command);
            var minifier = new Minifier();
            return minifier.MinifyStyleSheet(content, switchParser.CssSettings);
        }
    }
}
