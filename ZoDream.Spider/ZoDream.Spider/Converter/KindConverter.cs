using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using ZoDream.Core.EnumCollection;

namespace ZoDream.Spider.Converter
{
    /// <summary>
    /// 下载类型转换
    /// </summary>
    public class KindConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }
            Dictionary<FileKind, bool> temp = (Dictionary<FileKind, bool>)value;
            FileKind param = (FileKind)int.Parse(parameter.ToString());
            if (!temp.ContainsKey(param))
            {
                return false;
            }
            return temp[param];
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return null;
            }
            Dictionary<FileKind, bool> temp = new Dictionary<FileKind, bool>();
            temp.Add((FileKind)int.Parse(parameter.ToString()), (bool)value);
            return temp;
        }
    }
}
