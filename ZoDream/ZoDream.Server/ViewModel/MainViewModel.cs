using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using ZoDream.Server.Model;

namespace ZoDream.Server.ViewModel
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
        /// The <see cref="StartTime" /> property's name.
        /// </summary>
        public const string StartTimePropertyName = "StartTime";

        private DateTime _startTime = DateTime.Now;

        /// <summary>
        /// Sets and gets the StartTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                Set(StartTimePropertyName, ref _startTime, value);
            }
        }

        /// <summary>
        /// The <see cref="ListenPort" /> property's name.
        /// </summary>
        public const string ListenPortPropertyName = "ListenPort";

        private string _listenPort = "4440";

        /// <summary>
        /// Sets and gets the ListenPort property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ListenPort
        {
            get
            {
                return _listenPort;
            }
            set
            {
                Set(ListenPortPropertyName, ref _listenPort, value);
            }
        }

        /// <summary>
        /// The <see cref="UsersInformation" /> property's name.
        /// </summary>
        public const string UsersInformationPropertyName = "UsersInformation";

        private ObservableCollection<UserInformation> _usersInformation = new ObservableCollection<UserInformation>();

        /// <summary>
        /// Sets and gets the UsersInformation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<UserInformation> UsersInformation
        {
            get
            {
                return _usersInformation;
            }
            set
            {
                Set(UsersInformationPropertyName, ref _usersInformation, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            UsersInformation = new ObservableCollection<UserInformation>()
            {
                new UserInformation("123", "pc", "123.123.13.33")
            };
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}