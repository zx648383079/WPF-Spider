using System.Windows;
using ZoDream.Spider.ViewModel;

namespace ZoDream.Spider.View
{
    /// <summary>
    /// Description for NewView.
    /// </summary>
    public partial class NewView : Window
    {
        /// <summary>
        /// Initializes a new instance of the NewView class.
        /// </summary>
        public NewView()
        {
            InitializeComponent();
        }

        private void Window_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                this.Close();
            }
        }
    }
}