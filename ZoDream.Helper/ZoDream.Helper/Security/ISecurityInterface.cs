namespace ZoDream.Helper.Security
{
    public interface ISecurityInterface
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        string Decrypt(string content);

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        string Encrypt(string content);
    }
}
