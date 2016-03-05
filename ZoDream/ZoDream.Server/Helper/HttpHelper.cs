using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZoDream.Server.Model;

namespace ZoDream.Server.Helper
{
    /// <summary>
    /// http侦听
    /// </summary>
    public class HttpHelper
    {
        private HttpListener _listener;

        private HttpListenerContext _context;

        public HttpHelper()
        {
            
        }

        public HttpHelper(string prefix)
        {
            Begin(prefix);
        }

        public HttpHelper(string[] prefixes)
        {
            Begin(prefixes);
        }

        public void Begin(string prefix)
        {
            Begin(new string[] {prefix});
        }

        /// <summary>
        /// 开始启动
        /// </summary>
        /// <param name="prefixes"></param>
        public void Begin(string[] prefixes)
        {
            if (HttpListener.IsSupported && null == _listener && null != prefixes && 0 != prefixes.Length)
            {
                _listener = new HttpListener();
                foreach (string prefix in prefixes)
                {
                    _listener.Prefixes.Add(prefix);
                }
            }
        }

        public void AddCallback(AsyncCallback callback)
        {
            _listener.Start();
            IAsyncResult result = _listener.BeginGetContext(callback, _listener);
        }

        public string Request(IAsyncResult result)
        {
            HttpListener listener = (HttpListener) result.AsyncState;
            _context = listener.EndGetContext(result);
            HttpListenerRequest request = _context.Request;
            //List<HttpValue> values = new List<HttpValue>();
            //values.Add(new HttpValue("AcceptTypes", request.AcceptTypes.ToString()));
            string body;
            using (Stream input = request.InputStream)
            {
                byte[] buffer = new byte[request.ContentLength64];
                input.Read(buffer, 0, buffer.Length);
                input.Close();
                body = request.ContentEncoding.GetString(buffer);
                //values.Add(new HttpValue("Body", request.ContentEncoding.GetString(buffer)));
            }
            return body;
        }

        public void Response(string args)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(args);
            HttpListenerResponse response = _context.Response;
            response.ContentLength64 = buffer.Length;
            using (Stream output = response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            _context = null;
        }


        /// <summary>
        /// 停止
        /// </summary>
        public void End()
        {
            if (null != _listener)
            {
                _listener.Stop();
            }
        }
    }
}
