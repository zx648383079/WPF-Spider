using System.Windows;
using GalaSoft.MvvmLight.Threading;
using ZoDream.Finance.View;
using System.Configuration;

namespace ZoDream.Finance
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string isFirst = ConfigurationManager.AppSettings.Get("IsFirst");
            if (null == isFirst || bool.Parse(isFirst))
            {
                new DatabaseView().ShowDialog();
            }
        }
    }
}
