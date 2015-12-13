using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections;
using System.Collections.ObjectModel;
using ZoDream.Explorer.Helper;
using ZoDream.Explorer.Model;

namespace ZoDream.Explorer.ViewModel
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

        private string _title = "ZoDream Explorer";

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
        /// The <see cref="FileTree" /> property's name.
        /// </summary>
        public const string FileTreePropertyName = "FileTree";

        private ObservableCollection<FileInfoCollection> _fileTree = new ObservableCollection<FileInfoCollection>();

        /// <summary>
        /// Sets and gets the FileInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<FileInfoCollection> FileTree
        {
            get
            {
                return _fileTree;
            }
            set
            {
                Set(FileTreePropertyName, ref _fileTree, value);
            }
        }

        /// <summary>
        /// The <see cref="FileList" /> property's name.
        /// </summary>
        public const string FileListPropertyName = "FileList";

        private ObservableCollection<FileInfoCollection> _fileList = new ObservableCollection<FileInfoCollection>();

        /// <summary>
        /// Sets and gets the FileInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<FileInfoCollection> FileList
        {
            get
            {
                return _fileList;
            }
            set
            {
                Set(FileListPropertyName, ref _fileList, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ObservableCollection<FileInfoCollection> files = new ObservableCollection<FileInfoCollection>();
            FileList = FileDeal.GetRoot(ref files);
            foreach (FileInfoCollection item in files)
            {
                item.Children = FileDeal.GetDir(item.Path);
                FileTree.Add(item);
            }
        }

        private RelayCommand<FileInfoCollection> _selectedCommand;

        /// <summary>
        /// Gets the SelectedCommand.
        /// </summary>
        public RelayCommand<FileInfoCollection> SelectedCommand
        {
            get
            {
                return _selectedCommand
                    ?? (_selectedCommand = new RelayCommand<FileInfoCollection>(ExecuteSelectedCommand));
            }
        }

        private void ExecuteSelectedCommand(FileInfoCollection fileInfo)
        {
            Title = fileInfo.Name;
            FileList.Clear();
            if (fileInfo.Children != null)
            {
                foreach (FileInfoCollection item in fileInfo.Children)
                {
                    FileList.Add(item);
                }
            }
            _getFiles(fileInfo.Path);
        }

        private void _getFiles(string path)
        {
            string[] files = FileDeal.GetFile(path);
            if (files != null)
            {
                foreach (string item in files)
                {
                    FileList.Add(new FileInfoCollection(System.IO.Path.GetFileName(item), FileKinds.FILE, item));
                }
            }
        }

        private RelayCommand<FileInfoCollection> _doubleCommand;

        /// <summary>
        /// Gets the DoubleCommand.
        /// </summary>
        public RelayCommand<FileInfoCollection> DoubleCommand
        {
            get
            {
                return _doubleCommand
                    ?? (_doubleCommand = new RelayCommand<FileInfoCollection>(ExecuteDoubleCommand));
            }
        }

        private void ExecuteDoubleCommand(FileInfoCollection fileInfo)
        {
            if (fileInfo.Kind == FileKinds.DIR)
            {
                Message = null;
                Title = fileInfo.Name;
                FileList.Clear();
                FileList = FileDeal.GetDir(fileInfo.Path);
                _getFiles(fileInfo.Path);
            } else
            {
                Message = "您选择的是一个文件！此功能暂未开放！";
            }
        }

        private RelayCommand<IList> _selectedListCommand;

        /// <summary>
        /// Gets the SelectedListCommand.
        /// </summary>
        public RelayCommand<IList> SelectedListCommand
        {
            get
            {
                return _selectedListCommand
                    ?? (_selectedListCommand = new RelayCommand<IList>(ExecuteSelectedListCommand));
            }
        }

        private void ExecuteSelectedListCommand(IList args)
        {
            if (args.Count == 1)
            {
                FileInfoCollection fileInfo = args[0] as FileInfoCollection;
                Message = string.Format("您选择的 {0} 是一个{1}!", fileInfo.Name, fileInfo.Kind == FileKinds.DIR ? "文件夹" : "文件");
            } else if (args.Count != 0)
            {
                Message = string.Format("您选择了{0}个文件!", args.Count);
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}