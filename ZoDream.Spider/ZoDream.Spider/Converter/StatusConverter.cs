using System;
using System.Globalization;
using System.Windows.Data;
using ZoDream.Core.EnumCollection;

namespace ZoDream.Spider.Converter
{
    /// <summary>
    /// http状态转换
    /// </summary>
    [ValueConversion(typeof(HttpStatus), typeof(String))]
    public class StatusConverter : IValueConverter
    {
        /// <summary>
        /// 转换输出显示
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HttpStatus status = (HttpStatus)value;
            switch (status)
            {
                case HttpStatus.NONE:
                    return "无状态";
                case HttpStatus.WAITTING:
                    return "等待中……";
                case HttpStatus.USELESS:
                    return "已放弃！";
                case HttpStatus.DOWNLOADING:
                    return "下载中……";
                case HttpStatus.COMPLETE:
                    return "成功完成！";
                case HttpStatus.FAILED:
                    return "下载失败！";
                default:
                    return "未知状态";
            }
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
            throw new NotImplementedException();
        }
    }
}
