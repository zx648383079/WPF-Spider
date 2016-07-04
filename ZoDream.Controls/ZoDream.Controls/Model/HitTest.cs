using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoDream.Controls.Model
{
    /// <summary>  
    /// 包含了鼠标的各种消息  
    /// </summary>  
    public enum HitTest : int
    {
        /// <summary>  
        /// HTERROR  
        /// </summary>  
        HTERROR = -2,
        /// <summary>  
        /// HTTRANSPARENT  
        /// </summary>  
        HTTRANSPARENT = -1,
        /// <summary>  
        /// HTNOWHERE  
        /// </summary>  
        HTNOWHERE = 0,
        /// <summary>  
        /// HTCLIENT  
        /// </summary>  
        HTCLIENT = 1,
        /// <summary>  
        /// HTCAPTION  
        /// </summary>  
        HTCAPTION = 2,
        /// <summary>  
        /// HTSYSMENU  
        /// </summary>  
        HTSYSMENU = 3,
        /// <summary>  
        /// HTGROWBOX  
        /// </summary>  
        HTGROWBOX = 4,
        /// <summary>  
        /// HTSIZE  
        /// </summary>  
        HTSIZE = HTGROWBOX,
        /// <summary>  
        /// HTMENU  
        /// </summary>  
        HTMENU = 5,
        /// <summary>  
        /// HTHSCROLL  
        /// </summary>  
        HTHSCROLL = 6,
        /// <summary>  
        /// HTVSCROLL  
        /// </summary>  
        HTVSCROLL = 7,
        /// <summary>  
        /// HTMINBUTTON  
        /// </summary>  
        HTMINBUTTON = 8,
        /// <summary>  
        /// HTMAXBUTTON  
        /// </summary>  
        HTMAXBUTTON = 9,
        /// <summary>  
        /// HTLEFT  
        /// </summary>  
        HTLEFT = 10,
        /// <summary>  
        /// HTRIGHT  
        /// </summary>  
        HTRIGHT = 11,
        /// <summary>  
        /// HTTOP  
        /// </summary>  
        HTTOP = 12,
        /// <summary>  
        /// HTTOPLEFT  
        /// </summary>  
        HTTOPLEFT = 13,
        /// <summary>  
        /// HTTOPRIGHT  
        /// </summary>  
        HTTOPRIGHT = 14,
        /// <summary>  
        /// HTBOTTOM  
        /// </summary>  
        HTBOTTOM = 15,
        /// <summary>  
        /// HTBOTTOMLEFT  
        /// </summary>  
        HTBOTTOMLEFT = 16,
        /// <summary>  
        /// HTBOTTOMRIGHT  
        /// </summary>  
        HTBOTTOMRIGHT = 17,
        /// <summary>  
        /// HTBORDER  
        /// </summary>  
        HTBORDER = 18,
        /// <summary>  
        /// HTREDUCE  
        /// </summary>  
        HTREDUCE = HTMINBUTTON,
        /// <summary>  
        /// HTZOOM  
        /// </summary>  
        HTZOOM = HTMAXBUTTON,
        /// <summary>  
        /// HTSIZEFIRST  
        /// </summary>  
        HTSIZEFIRST = HTLEFT,
        /// <summary>  
        /// HTSIZELAST  
        /// </summary>  
        HTSIZELAST = HTBOTTOMRIGHT,
        /// <summary>  
        /// HTOBJECT  
        /// </summary>  
        HTOBJECT = 19,
        /// <summary>  
        /// HTCLOSE  
        /// </summary>  
        HTCLOSE = 20,
        /// <summary>  
        /// HTHELP  
        /// </summary>  
        HTHELP = 21
    }
}
