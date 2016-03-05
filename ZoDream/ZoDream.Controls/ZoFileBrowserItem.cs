using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    ///     <MyNamespace:ZoFileBrowserItem/>
    ///
    /// </summary>
    public class ZoFileBrowserItem : ContentControl
    {
        static ZoFileBrowserItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoFileBrowserItem), new FrameworkPropertyMetadata(typeof(ZoFileBrowserItem)));
        }

        public static DependencyProperty KindProperty = DependencyProperty.Register("Kind", typeof (Model.FileKind),
            typeof (ZoFileBrowserItem), null);

        public static DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof (string),
            typeof (ZoFileBrowserItem), null);

        public static DependencyProperty FullPathProperty = DependencyProperty.Register("FullPath", typeof (string),
            typeof (ZoFileBrowserItem), null);

        public static DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof (bool),
            typeof (ZoFileBrowserItem), null);

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public Model.FileKind Kind
        {
            get { return (Model.FileKind) GetValue(KindProperty); }
            set
            {
                SetValue(KindProperty, value);
                
            }
        }

        public string Name
        {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public string FullPath
        {
            get { return (string) GetValue(FullPathProperty); }
            set { SetValue(FullPathProperty, value); }
        }

        public ZoFileBrowserItem()
        {
            
        }

        public ZoFileBrowserItem(string fullPath)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(fullPath);
            FullPath = fullPath;
            Kind = Model.FileKind.DIR;
        }

        public ZoFileBrowserItem(string name, string fullPath)
        {
            Name = name;
            FullPath = fullPath;
            Kind = Model.FileKind.DIR;
        }

        public ZoFileBrowserItem(string fullPath, Model.FileKind kind)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(fullPath);
            FullPath = fullPath;
            Kind = kind;
        }

        public ZoFileBrowserItem(string name, string fullPath, Model.FileKind kind)
        {
            Name = name;
            FullPath = fullPath;
            Kind = kind;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            e.Handled = true;
            ZoFileBrowser parentZoFileBrowser = this.ParentZoFileBrowser;
            if (parentZoFileBrowser != null)
            {
                IsSelected = true;
                parentZoFileBrowser.NotifyZoFileBrowserItemSelected(this);
            }
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            e.Handled = true;
            ZoFileBrowser parentZoFileBrowser = this.ParentZoFileBrowser;
            if (parentZoFileBrowser != null)
            {
                parentZoFileBrowser.NotifyZoFileBrowserItemDoubleClick(this);
            }
            base.OnMouseDoubleClick(e);
        }


        private ZoFileBrowser ParentZoFileBrowser
            {
                get { return ParentSelector as ZoFileBrowser; }
            }

            internal Selector ParentSelector => ItemsControl.ItemsControlFromItemContainer(this) as Selector;
        }
}
