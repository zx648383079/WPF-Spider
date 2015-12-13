using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZoDream.Core.EnumCollection;
using ZoDream.Core.ModelCollection;

namespace ZoDream.Core.Helper.Data
{
    /// <summary>
    /// 对字符串进行正则处理，特指多个处理
    /// </summary>
    public class StringRegex
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="input"></param>
        /// <param name="patterns"></param>
        /// <returns></returns>
        public static string Excute(string input, IList<PatternParameter> patterns)
        {
            foreach (PatternParameter item in patterns)
            {
                if (item.Kind == PatternKind.COMMON)
                {
                    input.Replace(item.Pattern, item.Value);
                    continue;
                }
                Regex regex = new Regex(item.Pattern);
                switch (item.Kind)
                {
                    case PatternKind.MATCH:
                        Match m = regex.Match(input);
                        if (string.IsNullOrWhiteSpace(item.Value))
                        {
                            input = m.Value;
                        }
                        else
                        {
                            input = m.Groups[item.Value].Value;
                        }
                        break;
                    case PatternKind.REPLACE:
                        input = regex.Replace(input, item.Value);
                        break;
                    default:
                        break;
                }
            }
            return input;
        }
    }
}
