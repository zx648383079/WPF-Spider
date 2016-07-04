using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZoDream.Controls.Model;

namespace ZoDream.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZoDream.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZoDream.Controls;assembly=ZoDream.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:ZoWindow/>
    ///
    /// </summary>
    public class ZoWindow : Window
    {
        static ZoWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoWindow), new FrameworkPropertyMetadata(typeof(ZoWindow)));
        }

        /// <summary>
        /// 设置圆角属性
        /// </summary>
        public static DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius",
            typeof (CornerRadius), typeof (ZoWindow), null);
        /// <summary>
        /// 设置背景
        /// </summary>
        public static DependencyProperty ZoBackgroundProperty = DependencyProperty.Register("ZoBackground",
            typeof (Brush), typeof (ZoWindow), null);
        /// <summary>
        /// 设置菜单
        /// </summary>
        public static DependencyProperty ZoMenuProperty = DependencyProperty.Register("ZoMenu",
            typeof (System.Windows.Controls.ContextMenu), typeof (ZoWindow), null);

        /// <summary>
        /// 标题栏字体大小
        /// </summary>
        public static DependencyProperty TitleSizeProperty = DependencyProperty.Register("TitleSize", typeof (double),
            typeof (ZoWindow), null);

        private const int WM_NCHITTEST = 0x0084;
        private const int WM_GETMINMAXINFO = 0x0024;

        private const int CORNER_WIDTH = 12; //拐角宽度  
        private const int MARGIN_WIDTH = 4; // 边框宽度  
        private Point mMousePoint = new Point(); //鼠标坐标  
        private Button mMaxButton;

        /// <summary>
        /// 圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius) GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        /// <summary>
        /// 背景
        /// </summary>
        public Brush ZoBackground
        {
            get { return (Brush) GetValue(ZoBackgroundProperty); }
            set { SetValue(ZoBackgroundProperty, value); }
        }
        /// <summary>
        /// 菜单
        /// </summary>
        public System.Windows.Controls.ContextMenu ZoMenu
        {
            get { return (System.Windows.Controls.ContextMenu) GetValue(ZoMenuProperty); }
            set { SetValue(ZoMenuProperty, value); }
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        public double TitleSize
        {
            get { return (double) GetValue(TitleSizeProperty); }
            set { SetValue(TitleSizeProperty, value); }
        }

        /// <summary>  
        /// 窗口  
        /// </summary>  
        public ZoWindow()
        {

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Border windowBorder = GetTemplateChild("FussWindowBorder") as Border;
            windowBorder.MouseLeftButtonDown += (sender, e) => this.DragMove();

            Button optionButton = GetTemplateChild("OptionButton") as Button;
            if (this.ZoMenu != null)
            {
                optionButton.Visibility = Visibility.Visible;
                optionButton.Click += (sender, e) =>
                {
                    this.ZoMenu.IsOpen = true;
                };

            }

            Button miniButton = GetTemplateChild("MiniButton") as Button;
            miniButton.Click += (sender, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };

            Button maxButton = GetTemplateChild("MaxButton") as Button;
            maxButton.Click += (sender, e) =>
            {
                this.WindowState = (WindowState.Maximized == this.WindowState)
                    ? WindowState.Normal
                    : WindowState.Maximized;
            };

            Button closeButton = GetTemplateChild("CloseButton") as Button;
            closeButton.Click += (sender, e) => this.Close();


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
}
