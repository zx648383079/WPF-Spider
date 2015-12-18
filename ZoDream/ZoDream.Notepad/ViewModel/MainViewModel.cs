using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ZoDream.Notepad.Model;

namespace ZoDream.Notepad.ViewModel
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
        /// The <see cref="Title" /> property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        private string _title = "ZoDream Notepad";

        /// <summary>
        /// Sets and gets the Title property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                Set(TitlePropertyName, ref _title, value);
            }
        }

        private NotificationMessageAction<object> _navigate;

        private NotificationMessageAction _goBack;

        private NotificationMessageAction _goForward;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Messenger.Default.Register<NotificationMessageAction<object>>(this, "navigate", m =>
            {
                _navigate = m;
            });
            Messenger.Default.Register<NotificationMessageAction>(this, "goBack", m =>
            {
                _goBack = m;
            });
            Messenger.Default.Register<NotificationMessageAction>(this, "goForward", m =>
            {
                _goForward = m;
            });
        }

        private RelayCommand _fileCommand;

        /// <summary>
        /// Gets the FileCommand.
        /// </summary>
        public RelayCommand FileCommand
        {
            get
            {
                return _fileCommand
                    ?? (_fileCommand = new RelayCommand(ExecuteFileCommand));
            }
        }

        private void ExecuteFileCommand()
        {
            _navigate.Execute(new Pages.EditorPage());
        }

        private RelayCommand _openCommand;

        /// <summary>
        /// Gets the OpenCommand.
        /// </summary>
        public RelayCommand OpenCommand
        {
            get
            {
                return _openCommand
                    ?? (_openCommand = new RelayCommand(ExecuteOpenCommand));
            }
        }

        private void ExecuteOpenCommand()
        {
            _navigate.Execute(new Pages.ChoosePage());
        }

        private RelayCommand _goBackCommand;

        /// <summary>
        /// Gets the GoBackCommand.
        /// </summary>
        public RelayCommand GoBackCommand
        {
            get
            {
                return _goBackCommand
                    ?? (_goBackCommand = new RelayCommand(ExecuteGoBackCommand));
            }
        }

        private void ExecuteGoBackCommand()
        {
            _goBack.Execute();
        }

        private RelayCommand _homeCommand;

        /// <summary>
        /// Gets the HomeCommand.
        /// </summary>
        public RelayCommand HomeCommand
        {
            get
            {
                return _homeCommand
                    ?? (_homeCommand = new RelayCommand(ExecuteHomeCommand));
            }
        }

        private void ExecuteHomeCommand()
        {
            _navigate.Execute(new Pages.FilePage());
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}