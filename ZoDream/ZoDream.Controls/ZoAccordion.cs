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
    ///     <MyNamespace:ZoAccordion/>
    ///
    /// </summary>
    public class ZoAccordion : ItemsControl
    {
        static ZoAccordion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoAccordion), new FrameworkPropertyMetadata(typeof(ZoAccordion)));
        }

        #region ExpandedItem

        /// <summary>
        /// Gets/Sets which item to expand
        /// </summary>
        public object ExpandedItem
        {
            get { return (object)GetValue(ExpandedItemProperty); }
            set { SetValue(ExpandedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpandedItemProperty = DependencyProperty.Register(
            "ExpandedItem", typeof(object), typeof(ZoAccordion),
            new UIPropertyMetadata(null, new PropertyChangedCallback(OnExpandedItemChanged)));

        private static void OnExpandedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ZoAccordion shelf = sender as ZoAccordion;
            if (shelf != null)
            {
                shelf.OnExpandedItemChanged(e.OldValue, e.NewValue);
            }
        }

        protected virtual void OnExpandedItemChanged(object oldValue, object newValue)
        {
            ZoAccordionItem oldItem = this.ItemContainerGenerator.ContainerFromItem(oldValue) as ZoAccordionItem;

            if (oldItem != null)
            {
                oldItem.IsExpanded = false;
            }
        }

        #endregion

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ZoAccordionItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ZoAccordionItem;
        }
    }
}
