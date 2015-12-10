using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using ZoDream.Spider.View;
using System.Collections;
using ZoDream.Core.EnumCollection;
using ZoDream.Core.ModelCollection;
using ZoDream.Core.ActionCollection;
using ZoDream.Spider.Model;

namespace ZoDream.Spider.ViewModel
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
        /// The <see cref="FileInfos" /> property's name.
        /// </summary>
        public const string FileInfosPropertyName = "FileInfos";

        private ObservableCollection<FileParameter> _myProperty = new ObservableCollection<FileParameter>();

        /// <summary>
        /// Sets and gets the FileInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<FileParameter> FileInfos
        {
            get
            {
                return _myProperty;
            }
            set
            {
                Set(FileInfosPropertyName, ref _myProperty, value);
            }
        }

        /// <summary>
        /// The <see cref="Status" /> property's name.
        /// </summary>
        public const string StatusPropertyName = "Status";

        private HttpStatus _status = HttpStatus.NONE;

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public HttpStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                Set(StatusPropertyName, ref _status, value);
            }
        }

        /// <summary>
        /// The <see cref="Message" /> property's name.
        /// </summary>
        public const string MessagePropertyName = "Message";

        private string _message = null;

        /// <summary>
        /// Sets and gets the Message property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                Set(MessagePropertyName, ref _message, value);
            }
        }

        /// <summary>
        /// The <see cref="Progress" /> property's name.
        /// </summary>
        public const string ProgressPropertyName = "Progress";

        private int _progress = 0;

        /// <summary>
        /// Sets and gets the Progress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                Set(ProgressPropertyName, ref _progress, value);
            }
        }

        /// <summary>
        /// The <see cref="ProgessMessage" /> property's name.
        /// </summary>
        public const string ProgessMessagePropertyName = "ProgessMessage";

        private string _progessMessage = "暂未开始！";

        /// <summary>
        /// Sets and gets the ProgessMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ProgessMessage
        {
            get
            {
                return _progessMessage;
            }
            set
            {
                Set(ProgessMessagePropertyName, ref _progessMessage, value);
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            FileInfos = new ObservableCollection<FileParameter>();
            FileInfos.Add(new FileParameter(FileInfos.Count + 1, "http://blog.sina.com.cn/s/blog_4c0e8aa20100tm8c.html", "Downloads"));
            FileInfos.Add(new FileParameter(FileInfos.Count + 1, "http://www.jb51.net/jslib/syntaxhighlighter/styles/shCore.css", "Downloads"));
            FileInfos.Add(new FileParameter(FileInfos.Count + 1, "http://changyan.itc.cn/js/??lib/jquery.js,changyan.labs.js?appid=cyrEjJlIw", "Downloads"));
            FileInfos.Add(new FileParameter(FileInfos.Count + 1, "http://www.jb51.net/images/logo.gif", "Downloads"));
            FileInfos.Add(new FileParameter(FileInfos.Count + 1, "http://bdimg.share.baidu.com/static/api/js/share/share_api.js?v=226108fe.js", "Downloads"));
            FileInfos.Add(new FileParameter(FileInfos.Count + 1, "http://changyan.sohu.com/debug/cookie?callback=changyan576386101", "Downloads"));
        }


        private RelayCommand _newCommand;

        /// <summary>
        /// Gets the NewCommand.
        /// </summary>
        public RelayCommand NewCommand
        {
            get
            {
                return _newCommand
                    ?? (_newCommand = new RelayCommand(ExecuteNewCommand));
            }
        }

        private void ExecuteNewCommand()
        {
            new NewView().Show();
            Messenger.Default.Send(new NotificationMessageAction<bool>(null, ViewMessageCallback));
        }

        /// <summary>
        /// callback
        /// </summary>
        /// <param name="success"></param>
        public void ViewMessageCallback(bool success)
        {
            if (success)
            {
                Message = "新建任务完成，准备就绪！";
            }
        }
        

        private RelayCommand<IList> _selectionCommand;

        /// <summary>
        /// Gets the SelectionCommand.
        /// </summary>
        public RelayCommand<IList> SelectionCommand
        {
            get
            {
                return _selectionCommand
                    ?? (_selectionCommand = new RelayCommand<IList>(ExecuteSelectionCommand));
            }
        }

        private void ExecuteSelectionCommand(IList parameter)
        {
            Status = (parameter[0] as FileParameter).Status;
            Message = string.Format("选中{0}个", parameter.Count);
        }

        private RelayCommand _beginCommand;

        /// <summary>
        /// Gets the BeginCommand.
        /// </summary>
        public RelayCommand BeginCommand
        {
            get
            {
                return _beginCommand
                    ?? (_beginCommand = new RelayCommand(ExecuteBeginCommand));
            }
        }

        private void ExecuteBeginCommand()
        {
            Message = null;
            Status = HttpStatus.DOWNLOADING;
            Status = MainAction.Begin(Settings.Url, Settings.Path);
        }

        private RelayCommand _stopCommand;

        /// <summary>
        /// Gets the StopCommand.
        /// </summary>
        public RelayCommand StopCommand
        {
            get
            {
                return _stopCommand
                    ?? (_stopCommand = new RelayCommand(ExecuteStopCommand));
            }
        }

        private void ExecuteStopCommand()
        {

        }


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}