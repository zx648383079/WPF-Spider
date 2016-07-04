using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Control = System.Windows.Controls.Control;

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
    ///     <MyNamespace:ZoNotifyIcon/>
    ///
    /// </summary>
    public class ZoNotifyIcon : UIElement
    {
        private NotifyIcon _notifyIcon;

        static ZoNotifyIcon()
        {
            
        }

        public static DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof (string),
            typeof (ZoNotifyIcon), null);

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string),
            typeof (ZoNotifyIcon), null);

        public static DependencyProperty VisibleProperty = DependencyProperty.Register("Visible", typeof (bool),
            typeof (ZoNotifyIcon), null);

        public static readonly DependencyProperty ContextMenuProperty = DependencyProperty.Register("ContextMenu",
            typeof (System.Windows.Forms.ContextMenu), typeof (ZoNotifyIcon), null);


        public string Icon
        {
            get { return (string) GetValue(IconProperty); }
            set
            {
                if (!System.IO.Path.IsPathRooted(value))
                {
                    value = System.IO.Path.Combine(Environment.CurrentDirectory, value);
                }
                SetValue(IconProperty, value);
                _notifyIcon.Icon = new System.Drawing.Icon(value);
            }
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                _notifyIcon.Text = value;
            }
        }

        public bool Visible
        {
            get { return (bool) GetValue(VisibilityProperty); }
            set
            {
                SetValue(VisibilityProperty, value);
                _notifyIcon.Visible = value;
            }
        }

        public System.Windows.Forms.ContextMenu ContextMenu
        {
            get { return (System.Windows.Forms.ContextMenu) GetValue(ContextMenuProperty); }
            set
            {
                SetValue(ContextMenuProperty, value);
                _notifyIcon.ContextMenu = value;
            }
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        public ZoNotifyIcon()
        {
            _notifyIcon = new NotifyIcon();
        }
    }
}
