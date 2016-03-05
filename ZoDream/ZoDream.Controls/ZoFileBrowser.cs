using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    ///     <MyNamespace:ZoFileBrowser/>
    ///
    /// </summary>
    [TemplatePart(Name = "BeforeButton", Type = typeof(Button)),
    TemplatePart(Name = "ConfirmButton", Type = typeof(Button)),
    TemplatePart(Name = "CancelButton", Type = typeof(Button)),
    TemplatePart(Name = "PathTextBox", Type = typeof(TextBox)),
    TemplatePart(Name = "DropDownPopup", Type = typeof(Popup))]
    public class ZoFileBrowser :Selector
    {
        static ZoFileBrowser()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoFileBrowser), new FrameworkPropertyMetadata(typeof(ZoFileBrowser)));
        }

        /// <summary>
        /// 文件模式或文件夹模式
        /// </summary>
        public static DependencyProperty IsFileProperty = DependencyProperty.Register("IsFile", typeof (bool),
            typeof (ZoFileBrowser), null);

        /// <summary>
        /// 路径
        /// </summary>
        public static DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof (string),
            typeof (ZoFileBrowser), null);

        /// <summary>
        /// 是否打开选择器
        /// </summary>
        public static DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool),
            typeof(ZoFileBrowser), null);

        /// <summary>
        /// 无扩展名的文件名
        /// </summary>
        public static DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof (string),
            typeof (ZoFileBrowser), null);

        /// <summary>
        /// 拓展名
        /// </summary>
        public static DependencyProperty ExtensionProperty = DependencyProperty.Register("Extension", typeof (string),
            typeof (ZoFileBrowser), null);

        /// <summary>
        /// 是文件
        /// </summary>
        public bool IsFile
        {
            get { return (bool) GetValue(IsFileProperty); }
            set { SetValue(IsFileProperty, value); }
        }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            get { return (string) GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        /// <summary>
        /// 下拉选择框是否打开
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return (string) GetValue(FileNameProperty);
            }
            set { SetValue(FileNameProperty, value); }
        }

        /// <summary>
        /// 扩展名 用 ; 分割
        /// </summary>
        public string Extension
        {
            get { return (string) GetValue(ExtensionProperty); }
            set { SetValue(ExtensionProperty, value); }
        }

        /// <summary>
        /// 准备指定元素以显示指定项。
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ZoFileBrowserItem();
        }
        /// <summary>
        /// 如果项是其自己的容器（或可以作为自己的容器），则为 true；否则为 false。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ZoFileBrowserItem;
        }

        internal void NotifyZoFileBrowserItemSelected(ZoFileBrowserItem item)
        {
            if (item != null)
            {
                ZoFileBrowserItem fileBrowserItem = this.SelectedItem as ZoFileBrowserItem;
                if (fileBrowserItem != null)
                    fileBrowserItem.IsSelected = false;
                _setSelected(item);
            }
        }

        internal void NotifyZoFileBrowserItemDoubleClick(ZoFileBrowserItem item)
        {
            if (null == item) return;
            if (Model.FileKind.FILE == item.Kind)
            {
                Path = item.FullPath;
                IsOpen = false;
            }
            else
            {
                Path = item.FullPath;
                _clearSelected();
                _getPath();
            }
        }

        /// <summary>
        /// 应用模板时发生
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TextBox pathTextBox = GetTemplateChild("PathTextBox") as TextBox;
            if (pathTextBox != null) pathTextBox.GotFocus += PathTextBox_GotFocus;
            Button confirmButton = GetTemplateChild("ConfirmButton") as Button;
            if (confirmButton != null) confirmButton.Click += ConfirmButton_Click;

            Button cancelButton = GetTemplateChild("CancelButton") as Button;
            if (cancelButton != null) cancelButton.Click += CancelButton_Click;
            _getPath();
        }

        private void _setSelected(ZoFileBrowserItem item)
        {
            for (int i = this.Items.Count - 1; i >= 0; i--)
            {
                if (Equals(this.Items[i], item))
                {
                    this.SelectedIndex = i;
                    break;
                }
            }
            this.SelectedItem = item;
            this.SelectedValue = item.FullPath;
            this.SelectedValuePath = "FullPath";
        }

        private void _clearSelected()
        {
            this.SelectedItem = null;
            this.SelectedValue = null;
            this.SelectedIndex = -1;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsOpen)
            {
                IsOpen = false;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsOpen) return;
            ZoFileBrowserItem item = this.SelectedItem as ZoFileBrowserItem;
            if (item != null)
                Path = item.FullPath;
            IsOpen = false;
        }

        private void PathTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!IsOpen)
            {
                IsOpen = true;
            }

        }

        private void _getPath()
        {
            this.Items.Clear();
            if (string.IsNullOrWhiteSpace(Path))
            {
                _getRoot();
            }
            else
            {
                _getAllFile();
            }
        }

        private void _getRoot()
        {
            DriveInfo[] infos = DriveInfo.GetDrives();
            foreach (DriveInfo item in infos)
            {
                if (item.DriveType != DriveType.Fixed && item.DriveType != DriveType.Removable) continue;
                string name = item.Name;
                this.Items.Add(new ZoFileBrowserItem(string.Format("{0}({1})", item.VolumeLabel, name.TrimEnd('\\')), name, Model.FileKind.DIR));
            }
        }

        private void _getAllFile()
        {
            if (!Directory.Exists(Path)) return;
            try
            {
                string[] paths = Directory.GetDirectories(Path);
                foreach (string item in paths)
                {
                    this.Items.Add(new ZoFileBrowserItem(item, Model.FileKind.DIR));
                }
            }
            catch (System.Exception)
            {
                // ignored
            }

            if (!IsFile) return;
            {
                try
                {
                    string[] paths = Directory.GetFiles(Path);
                    foreach (string item in paths)
                    {
                        this.Items.Add(new ZoFileBrowserItem(item, Model.FileKind.DIR));
                    }
                }
                catch (System.Exception)
                {
                    // ignored
                }
            }
        }
    }
}
