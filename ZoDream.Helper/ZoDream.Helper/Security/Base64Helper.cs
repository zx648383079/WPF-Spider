using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Helper.Security
{
    public class Base64Helper : ISecurityInterface
    {

        public string Decrypt(string content)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(content));
        }

        public string Encrypt(string content)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(content));
        }
    }
}
