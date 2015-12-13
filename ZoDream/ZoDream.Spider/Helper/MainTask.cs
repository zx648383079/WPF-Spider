using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoDream.Core.EnumCollection;
using ZoDream.Core.ModelCollection;

namespace ZoDream.Spider.Helper
{
    /// <summary>
    /// 主线程
    /// </summary>
    public class MainTask
    {
        /// <summary>
        /// 开始
        /// </summary>
        public static void Begin(FileParameter arg)
        {
            new MainTask(arg);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="arg"></param>
        public MainTask(FileParameter arg)
        {
            _file = arg;
            this._down();
        }

        private FileParameter _file;

        /// <summary>
        /// 下载
        /// </summary>
        private void _down()
        {
            switch (_file.Kind)
            {
                case FileKind.Html:
                /*case FileKind.Js:
                    break;*/
                case FileKind.Css:
                    
                    break;
                case FileKind.Image:
                    break;
                case FileKind.Video:
                    break;
                case FileKind.Audio:
                    break;
                case FileKind.File:
                    break;
                case FileKind.Unkown:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 正则筛选
        /// </summary>
        private void _regex()
        {

        }

        /// <summary>
        /// 提取url
        /// </summary>
        private void _getUrl()
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        private void _save()
        {

        }
    }
}
