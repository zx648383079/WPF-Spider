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
    ///     <MyNamespace:ZoAccordionItem/>
    ///
    /// </summary>
    public class ZoAccordionItem : HeaderedContentControl
    {
        static ZoAccordionItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoAccordionItem), new FrameworkPropertyMetadata(typeof(ZoAccordionItem)));
            CommandBinding expandCommandBinding = new CommandBinding(ExpandCommand, OnExecuteExpand, CanExecuteExpand);
            CommandManager.RegisterClassCommandBinding(typeof(ZoAccordionItem), expandCommandBinding);
        }

        #region IsExpanded

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
            "IsExpanded", typeof(bool), typeof(ZoAccordionItem), new PropertyMetadata(false, new PropertyChangedCallback(OnIsExpandedChanged)));

        private static void OnIsExpandedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ZoAccordionItem item = sender as ZoAccordionItem;
            if (item != null)
            {
                item.OnIsExpandedChanged(e);
            }
        }

        protected virtual void OnIsExpandedChanged(DependencyPropertyChangedEventArgs e)
        {
            bool newValue = (bool)e.NewValue;

            if (newValue)
            {
                this.OnExpanded();
            }
            else
            {
                this.OnCollapsed();
            }
        }

        #endregion

        #region Expand Events

        /// <summary>
        /// Raised when selected
        /// </summary>
        public event RoutedEventHandler Expanded
        {
            add { AddHandler(ExpandedEvent, value); }
            remove { RemoveHandler(ExpandedEvent, value); }
        }

        public static RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent(
            "Expanded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ZoAccordionItem));

        /// <summary>
        /// Raised when unselected
        /// </summary>
        public event RoutedEventHandler Collapsed
        {
            add { AddHandler(CollapsedEvent, value); }
            remove { RemoveHandler(CollapsedEvent, value); }
        }

        public static RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent(
            "Collapsed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ZoAccordionItem));

        protected virtual void OnExpanded()
        {
            ZoAccordion parentAccordian = this.ParentZoAccordian;
            if (parentAccordian != null)
            {
                parentAccordian.ExpandedItem = this;
            }
            RaiseEvent(new RoutedEventArgs(ExpandedEvent, this));
        }

        protected virtual void OnCollapsed()
        {
            RaiseEvent(new RoutedEventArgs(CollapsedEvent, this));
        }

        #endregion

        #region ExpandCommand

        public static RoutedCommand ExpandCommand = new RoutedCommand("Expand", typeof(ZoAccordionItem));

        private static void OnExecuteExpand(object sender, ExecutedRoutedEventArgs e)
        {
            ZoAccordionItem item = sender as ZoAccordionItem;
            if (!item.IsExpanded)
            {
                item.IsExpanded = true;
            }
        }

        private static void CanExecuteExpand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = sender is ZoAccordionItem;
        }

        #endregion

        #region ParentAccordian

        private ZoAccordion ParentZoAccordian
        {
            get { return ItemsControl.ItemsControlFromItemContainer(this) as ZoAccordion; }
        }

        #endregion
    }
}
