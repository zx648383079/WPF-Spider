using System.Windows;

namespace ZoDream.ToolBox.View
{
    /// <summary>
    /// Description for BallView.
    /// </summary>
    public partial class BallView : Window
    {
        /// <summary>
        /// Initializes a new instance of the BallView class.
        /// </summary>
        public BallView()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}