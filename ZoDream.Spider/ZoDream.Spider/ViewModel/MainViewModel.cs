using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using ZoDream.Spider.Model;
using ZoDream.Spider.View;
using ZoDream.Core.Import;
using System.Collections;

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

        private ObservableCollection<FileInfo> _myProperty = new ObservableCollection<FileInfo>();

        /// <summary>
        /// Sets and gets the FileInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<FileInfo> FileInfos
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
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            FileInfos = new ObservableCollection<FileInfo>();
            FileInfos.Add(new FileInfo(FileInfos.Count + 1, "http://blog.sina.com.cn/s/blog_4c0e8aa20100tm8c.html", "Downloads"));
            FileInfos.Add(new FileInfo(FileInfos.Count + 1, "http://www.jb51.net/jslib/syntaxhighlighter/styles/shCore.css", "Downloads"));
            FileInfos.Add(new FileInfo(FileInfos.Count + 1, "http://changyan.itc.cn/js/??lib/jquery.js,changyan.labs.js?appid=cyrEjJlIw", "Downloads"));
            FileInfos.Add(new FileInfo(FileInfos.Count + 1, "http://www.jb51.net/images/logo.gif", "Downloads"));
            FileInfos.Add(new FileInfo(FileInfos.Count + 1, "http://bdimg.share.baidu.com/static/api/js/share/share_api.js?v=226108fe.js", "Downloads"));
            FileInfos.Add(new FileInfo(FileInfos.Count + 1, "http://changyan.sohu.com/debug/cookie?callback=changyan576386101", "Downloads"));
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
                Message = "新建任务完成，即将开始";
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
            Status = (parameter[0] as FileInfo).Status;
            Message = string.Format("选中{0}个", parameter.Count);
        }


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}