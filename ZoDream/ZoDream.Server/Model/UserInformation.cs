using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Server.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInformation
    {
        private string _account;
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        private string _driver;
        /// <summary>
        /// 设备类型
        /// </summary>
        public string Driver
        {
            get { return _driver; }
            set { _driver = value; }
        }

        private string _address;
        /// <summary>
        /// 用户地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private DateTime _signTime = DateTime.Now;
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime SignTime
        {
            get { return _signTime; }
            set { _signTime = value; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="account"></param>
        /// <param name="driver"></param>
        /// <param name="address"></param>
        public UserInformation(string account, string driver, string address)
        {
            this.Account = account;
            this.Driver = driver;
            this.Address = address;
        }
    }
}
