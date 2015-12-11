using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace ZoDream.Explorer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
