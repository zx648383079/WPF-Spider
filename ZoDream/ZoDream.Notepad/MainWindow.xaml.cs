using GalaSoft.MvvmLight.Messaging;
using System.Windows.Navigation;
using ZoDream.Notepad.ViewModel;

namespace ZoDream.Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            Messenger.Default.Send(new NotificationMessageAction<object>(null, _navigate), "navigate");
            Messenger.Default.Send(new NotificationMessageAction(null, _goBack), "goBack");
            Messenger.Default.Send(new NotificationMessageAction(null, _goForward), "goForward");
        }

        private void _navigate(object root)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(root);
            }
        }

        private void _goBack()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void _goForward()
        {
            if (NavigationService.CanGoForward)
            {
                NavigationService.GoForward();
            }
        }
    }
}