using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.ControlBrowser.Comparer
{
    /// <summary>
    /// Type比较器
    /// </summary>
    public class TypeComparer : IComparer<Type>
    {
        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Type x, Type y)
        {
            return String.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}
