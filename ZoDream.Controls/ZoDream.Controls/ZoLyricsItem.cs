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
    ///     <MyNamespace:ZoLyricsItem/>
    ///
    /// </summary>
    public class ZoLyricsItem : Control
    {
        static ZoLyricsItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoLyricsItem), new FrameworkPropertyMetadata(typeof(ZoLyricsItem)));
        }

        /// <summary>
        /// 歌词
        /// </summary>
        public static readonly DependencyProperty LyricsProperty = DependencyProperty.Register("Lyrics", typeof (string),
            typeof (ZoLyricsItem), null);

        /// <summary>
        /// 状态
        /// </summary>
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof (LyricsStatus),
            typeof (ZoLyricsItem), null);
        /// <summary>
        /// 当前语句的开始时间
        /// </summary>
        public static readonly DependencyProperty BeginTimeProperty = DependencyProperty.Register("BeginTime",
            typeof (TimeSpan), typeof (ZoLyricsItem), null);
        /// <summary>
        /// 当前语句的时间跨度
        /// </summary>
        public static readonly DependencyProperty LengthTimeProperty = DependencyProperty.Register("LengthTime",
            typeof (TimeSpan), typeof (ZoLyricsItem), null);

        /// <summary>
        /// 当前语句的开始时间
        /// </summary>
        public TimeSpan BeginTime
        {
            get { return (TimeSpan) GetValue(BeginTimeProperty); }
            set { SetValue(BeginTimeProperty, value); }
        }

        /// <summary>
        /// 当前语句的时间跨度
        /// </summary>
        public TimeSpan LengthTime
        {
            get { return (TimeSpan) GetValue(LengthTimeProperty); }
            set { SetValue(LengthTimeProperty, value); }
        }

        /// <summary>
        /// 歌词
        /// </summary>
        public string Lyrics
        {
            get { return (string) GetValue(LyricsProperty); }
            set { SetValue(LyricsProperty, value); }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public LyricsStatus Status
        {
            get { return (LyricsStatus) GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        private void _statusChang()
        {
            
        }

        private ZoLyrics ParentZoLyrics
        {
            get { return ItemsControl.ItemsControlFromItemContainer(this) as ZoLyrics; }
        }
    }
}
