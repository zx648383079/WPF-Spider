using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using ZoDream.ControlBrowser.ViewModel;

namespace ZoDream.ControlBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Send(new NotificationMessageAction<UIElement>(null, _add), "add");
            Messenger.Default.Send(new NotificationMessageAction<UIElement>(null, _remove), "remove");
        }

        private void _add(UIElement control)
        {
            LayoutRoot.Children.Add(control);
        }

        private void _remove(UIElement control)
        {
            LayoutRoot.Children.Remove(control);
        }
    }
}