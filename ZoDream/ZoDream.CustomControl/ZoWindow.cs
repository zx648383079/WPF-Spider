﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ZoDream.CustomControl
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZoDream.CustomControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZoDream.CustomControl;assembly=ZoDream.CustomControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ZoWindow/>
    ///
    /// </summary>
    public class ZoWindow : System.Windows.Window
    {
        private readonly ResourceDictionary mWindowResouce = new ResourceDictionary();
        private readonly ControlTemplate mTemplate;

        private const int WM_NCHITTEST = 0x0084;
        private const int WM_GETMINMAXINFO = 0x0024;

        private const int CORNER_WIDTH = 12; //拐角宽度  
        private const int MARGIN_WIDTH = 4; // 边框宽度  
        private Point mMousePoint = new Point(); //鼠标坐标  
        private Button mMaxButton;
        private bool mIsShowMax = true;

        /// <summary>  
        /// 是否显示最大化按钮  
        /// </summary>  
        public bool IsShowMax
        {
            get
            {
                return mIsShowMax;
            }
            set
            {
                mIsShowMax = value;
            }
        }

        private BackGroundType mBackGroundType;
        /// <summary>  
        /// 背景类型  
        /// </summary>  
        public BackGroundType BackGroundType
        {
            get
            {
                return mBackGroundType;
            }
            set
            {
                mBackGroundType = value;
            }
        }

        private ImageSource mBackImage;
        /// <summary>  
        /// 背景图片  
        /// </summary>  
        public ImageSource BackImage
        {
            get
            {
                return mBackImage;
            }
            set
            {
                mBackImage = value;
            }
        }

        /// <summary>  
        /// 窗口  
        /// </summary>  
        public ZoWindow()
        {
            mWindowResouce.Source = new Uri("XJControls;component/Themes/ZoTheme.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Add(mWindowResouce);
            this.Style = (Style)mWindowResouce["WindowStyle"];
            var windowTemplate = (ControlTemplate)mWindowResouce["WindowTemplate"];
            this.Template = windowTemplate;
            mTemplate = windowTemplate;
            this.Loaded += new RoutedEventHandler(WindowBase_Loaded);
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            mBackGroundType = BackGroundType.Blue;
        }

        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            ((Border)mTemplate.FindName("FussWindowBorder", this)).MouseLeftButtonDown += (s1, e1) => this.DragMove();
            ((TextBlock)mTemplate.FindName("TitleText", this)).Text = this.Title;
            ((Image)mTemplate.FindName("ImgApp", this)).Source = this.Icon;

            var backBorder = (Border)mTemplate.FindName("BorderBack", this);
            var headBorder = (Border)mTemplate.FindName("TitleBar", this);
            switch (mBackGroundType)
            {
                case BackGroundType.Green:
                    backBorder.Style = (Style)mWindowResouce["BackStyleWhite"];
                    headBorder.Style = (Style)mWindowResouce["HeadStyleGreen"];
                    break;
                case BackGroundType.Blue:
                    backBorder.Style = (Style)mWindowResouce["BackStyleWhite"];
                    headBorder.Style = (Style)mWindowResouce["HeadStyleBlue"];
                    break;
                case BackGroundType.Image:
                    backBorder.Style = (Style)mWindowResouce["BackStyleImage"];
                    backBorder.Background = new ImageBrush(mBackImage);
                    headBorder.Style = (Style)mWindowResouce["HeadStyleTransparent"];
                    break;
                default:
                    backBorder.Style = (Style)mWindowResouce["BackStyleWhite"];
                    headBorder.Style = (Style)mWindowResouce["HeadStyleGreen"];
                    break;
            }

            mMaxButton = (Button)mTemplate.FindName("MaxButton", this);
            if (!IsShowMax)
            {
                mMaxButton.Visibility = Visibility.Collapsed;
                var rectangle = mTemplate.FindName("MaxSplitter", this) as Rectangle;
                if (rectangle != null)
                    rectangle.Visibility = Visibility.Collapsed;
            }
            else mMaxButton.Visibility = Visibility.Visible;

            this.SizeChanged += (s1, e1) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    mMaxButton.Style = (Style)mWindowResouce["WinNormalButton"];
                }
                else if (mMaxButton.Style != (Style)mWindowResouce["WinMaxButton"]
                    && this.WindowState == WindowState.Maximized)
                {
                    mMaxButton.Style = (Style)mWindowResouce["WinMaxButton"];
                }
            };

            ((Button)mTemplate.FindName("MiniButton", this)).Click += (s2, e2) =>
            {
                this.WindowState = WindowState.Minimized;
            };
            mMaxButton.Click += (s3, e3) =>
            {
                this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
            };

            ((Button)mTemplate.FindName("CloseButton", this)).Click += (s4, e4) => this.Close();

            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }
        }
        /// <summary>  
        /// 窗口消息  
        /// </summary>  
        /// <param name="hwnd"></param>  
        /// <param name="msg"></param>  
        /// <param name="wParam"></param>  
        /// <param name="lParam"></param>  
        /// <param name="handled"></param>  
        /// <returns></returns>  
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_NCHITTEST:
                    mMousePoint.X = (lParam.ToInt32() & 0xFFFF);
                    mMousePoint.Y = (lParam.ToInt32() >> 16);
                    handled = true;
                    //窗体为最大化时（如果最大化，Left、Top属性都会造成影响）  
                    if (this.WindowState == WindowState.Normal)
                    {
                        #region 拖拽改变窗体大小  
                        //左上角  
                        if ((mMousePoint.Y - Top <= CORNER_WIDTH) && (mMousePoint.X - Left <= CORNER_WIDTH))
                        {
                            return new IntPtr((int)HitTest.HTTOPLEFT);
                        }
                        //左下角  
                        if ((this.ActualHeight + this.Top - this.mMousePoint.Y <= CORNER_WIDTH) && (this.mMousePoint.X - this.Left <= CORNER_WIDTH))
                        {
                            return new IntPtr((int)HitTest.HTBOTTOMLEFT);
                        }
                        //右上角  
                        if ((this.mMousePoint.Y - this.Top <= CORNER_WIDTH) && (this.ActualWidth + this.Left - this.mMousePoint.X <= CORNER_WIDTH))
                        {
                            return new IntPtr((int)HitTest.HTTOPRIGHT);
                        }
                        //右下角  
                        if ((this.ActualWidth + this.Left - this.mMousePoint.X <= CORNER_WIDTH) && (this.ActualHeight + this.Top - this.mMousePoint.Y <= CORNER_WIDTH))
                        {
                            return new IntPtr((int)HitTest.HTBOTTOMRIGHT);
                        }
                        //左侧  
                        if (this.mMousePoint.X - (this.Left + 4) <= MARGIN_WIDTH)
                        {
                            return new IntPtr((int)HitTest.HTLEFT);
                        }
                        //右侧  
                        if (this.ActualWidth + this.Left - 4 - this.mMousePoint.X <= MARGIN_WIDTH)
                        {
                            return new IntPtr((int)HitTest.HTRIGHT);
                        }
                        //上侧  
                        if (this.mMousePoint.Y - (this.Top + 4) <= MARGIN_WIDTH)
                        {
                            return new IntPtr((int)HitTest.HTTOP);
                        }
                        //下侧  
                        if (this.ActualHeight + this.Top - 4 - this.mMousePoint.Y <= MARGIN_WIDTH)
                        {
                            return new IntPtr((int)HitTest.HTBOTTOM);
                        }
                        #endregion
                        //正常情况下移动窗体  
                        if (this.mMousePoint.Y - this.Top > 0 && this.mMousePoint.Y - this.Top < 25 && this.Left + this.ActualWidth - mMousePoint.X > 100)
                        {
                            return new IntPtr((int)HitTest.HTCAPTION);
                        }
                    }
                    //最大化时移动窗体，让窗体正常化  
                    if (this.WindowState == WindowState.Maximized)
                    {
                        if (this.mMousePoint.Y < 40 && this.ActualWidth - mMousePoint.X > 110)
                        {
                            return new IntPtr((int)HitTest.HTCAPTION);
                        }
                    }
                    //将q其他区域设置为客户端，避免鼠标事件被屏蔽  
                    return new IntPtr((int)HitTest.HTCLIENT);
            }
            return IntPtr.Zero;
        }
    }

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
    /// <summary>  
    /// 背景类型  
    /// </summary>  
    public enum BackGroundType
    {
        /// <summary>  
        /// 绿色调  
        /// </summary>  
        Green,
        /// <summary>  
        /// 蓝色调  
        /// </summary>  
        Blue,
        /// <summary>  
        /// 背景图片  
        /// </summary>  
        Image
    }
}
