using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZoDream.Core.Helper.Data
{
    /// <summary>
    /// 数据验证
    /// </summary>
    public class Validator
    {
        #region DataValidator
        /// <summary>
        /// 空构造函数
        /// </summary>
        public Validator()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region IsInt32
        /// <summary>
        /// 验证字符串是否整数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsInt32(string input)
        {
            string regexString = @"^-?\\d+$";

            return Regex.IsMatch(input, regexString);
        }
        #endregion

        #region IsDouble
        /// <summary>
        /// 验证字符串是否浮点数字
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsDouble(string input)
        {
            string regexString = @"^(-?\\d+)(\\.\\d+)?$";

            return Regex.IsMatch(input, regexString);
        }
        #endregion

        #region IsEmail
        /// <summary>
        /// 验证字符串Email地址
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            string regexString = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            return Regex.IsMatch(input, regexString);
        }
        #endregion

        #region IsDate
        /// <summary>
        /// 验证字符串是否日期[2004-2-29|||2004-02-29 10:29:39 pm|||2004/12/31]
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsDate(string input)
        {
            string regexString = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$";

            return Regex.IsMatch(input, regexString);
        }
        #endregion

        #region IsAnsiSqlDate
        /// <summary>
        /// 验证字符串是否 ANSI SQL date format
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsAnsiSqlDate(string input)
        {
            string regexString = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578]
                                )|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[4
                                69])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\
                                s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([1
                                3579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((
                                0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((
                                0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9]
                                )|(2[0-8]))))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(
                                \:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$";

            return Regex.IsMatch(input, regexString);
        }
        #endregion

        #region IsTxtFileName
        /// <summary>
        /// 验证字符串是否TXT文件名(全名)
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsTxtFileName(string input)
        {
            string regexString = @"^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*))+\.(txt|TXT)$";
            return Regex.IsMatch(input, regexString);
        }
        #endregion

        /// <summary>
        /// 验证是否是Url
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUrl(string input)
        {
            return Uri.IsWellFormedUriString(input, UriKind.Absolute);
        }

        /// <summary>
        /// 验证url是否有效
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidUrl(string input)
        {
            return IsUrl(input);
            /*if (IsUrl(input))
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(input);
                return reply.Status.Equals(IPStatus.Success);
            }
            return false;*/
        }

        /// <summary>  
        /// 根据主机名（域名）获得主机的IP地址  
        /// </summary>  
        /// <param name="hostName">主机名或域名</param>  
        /// <example> GetIPByDomain("www.google.com");</example>  
        /// <returns>主机的IP地址</returns>  
        public string GetIpByHostName(string hostName)
        {
            hostName = hostName.Trim();
            if (hostName == string.Empty)
                return string.Empty;
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(hostName);
                return host.AddressList.GetValue(0).ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
}
