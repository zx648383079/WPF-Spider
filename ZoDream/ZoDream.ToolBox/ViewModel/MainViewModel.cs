using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using ZoDream.ToolBox.Model;

namespace ZoDream.ToolBox.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="ToolsInformation" /> property's name.
        /// </summary>
        public const string ToolsInformationPropertyName = "ToolsInformation";

        private ObservableCollection<ToolInformation> _myProperty = new ObservableCollection<ToolInformation>();

        /// <summary>
        /// Sets and gets the ToolsInformation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<ToolInformation> ToolsInformation
        {
            get
            {
                return _myProperty;
            }
            set
            {
                Set(ToolsInformationPropertyName, ref _myProperty, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ToolsInformation = new ObservableCollection<ToolInformation>()
            {
                new ToolInformation("c:\\gggg.txt"),
                new ToolInformation("c:\\fff.txt"),
                new ToolInformation("c:\\ooo.txt")
            };
            new View.UsualView().Show();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}