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

namespace ZoDream.Notepad.Pages
{
    /// <summary>
    /// Interaction logic for EditorPage.xaml
    /// </summary>
    public partial class EditorPage : Page
    {
        public EditorPage()
        {
            InitializeComponent();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            _navigate(new AboutPage());
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            _navigate(new FilePage());
        }

        private void _navigate(object root)
        {
            if (NavigationService != null)
            {
                NavigationService.Navigate(root);
            }
        }
    }
}
