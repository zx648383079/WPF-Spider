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
    ///     <MyNamespace:ZoGroupList/>
    ///
    /// </summary>
    public class ZoGroupList : HeaderedItemsControl
    {
        static ZoGroupList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoGroupList), new FrameworkPropertyMetadata(typeof(ZoGroupList)));
            CommandBinding expandCommandBinding = new CommandBinding(ExpandCommand, OnExecuteExpand, CanExecuteExpand);
            CommandManager.RegisterClassCommandBinding(typeof(ZoGroupList), expandCommandBinding);
        }

        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
            "IsExpanded", typeof(bool), typeof(ZoGroupListItem), null);

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        #region ExpandCommand

        public static RoutedCommand ExpandCommand = new RoutedCommand("Expand", typeof(ZoGroupList));

        private static void OnExecuteExpand(object sender, ExecutedRoutedEventArgs e)
        {
            ZoGroupList item = sender as ZoGroupList;
            if (!item.IsExpanded)
            {
                item.IsExpanded = true;
            }
        }

        private static void CanExecuteExpand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = sender is ZoGroupList;
        }

        #endregion

        private ZoGroup ParentZoGroup
        {
            get { return ItemsControl.ItemsControlFromItemContainer(this) as ZoGroup; }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ZoGroupListItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ZoGroupListItem;
        }
    }
}
