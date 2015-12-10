using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ZoDream.Core.EnumCollection;
using ZoDream.Core.Helper.Data;
using ZoDream.Spider.Model;

namespace ZoDream.Spider.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class NewViewModel : ViewModelBase
    {
        private NotificationMessageAction<bool> _viewMessage;

        /// <summary>
        /// Initializes a new instance of the NewViewModel class.
        /// </summary>
        public NewViewModel()
        {
            Messenger.Default.Register<NotificationMessageAction<bool>>(this, _message );
            ExecuteDefaultCommand();
        }

        private void _message(NotificationMessageAction<bool> m)
        {
            _viewMessage = m;
        }

        /// <summary>
        /// The <see cref="ViewEnable" /> property's name.
        /// </summary>
        public const string ViewEnablePropertyName = "ViewEnable";

        private bool _viewEnable = true;

        /// <summary>
        /// Sets and gets the ViewEnable property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ViewEnable
        {
            get
            {
                return _viewEnable;
            }
            set
            {
                Set(ViewEnablePropertyName, ref _viewEnable, value);
            }
        }

        /// <summary>
        /// The <see cref="Url" /> property's name.
        /// </summary>
        public const string UrlPropertyName = "Url";

        private string _url = string.Empty;

        /// <summary>
        /// Sets and gets the Url property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                Set(UrlPropertyName, ref _url, value);
            }
        }

        /// <summary>
        /// The <see cref="DownLoadMode" /> property's name.
        /// </summary>
        public const string ModePropertyName = "DownLoadMode";

        private DownMode _mode = DownMode.ALL;

        /// <summary>
        /// Sets and gets the Mode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DownMode DownLoadMode
        {
            get
            {
                return _mode;
            }
            set
            {
                if (value != null)
                {
                    Set(ModePropertyName, ref _mode, value);
                }
            }
        }

        /// <summary>
        /// The <see cref="Kind" /> property's name.
        /// </summary>
        public const string KindPropertyName = "Kind";

        private Dictionary<FileKind, bool> _kind = new Dictionary<FileKind, bool>();

        /// <summary>
        /// Sets and gets the Kind property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Dictionary<FileKind, bool> Kind
        {
            get
            {
                return _kind;
            }
            set
            {
                if (value != null)
                {
                    var oldValue = _kind;
                    foreach (FileKind item in value.Keys)
                    {
                        _kind[item] = value[item];
                    }
                    RaisePropertyChanged(KindPropertyName, oldValue, value, true);
                }
            }
        }

        /// <summary>
        /// The <see cref="Depth" /> property's name.
        /// </summary>
        public const string DepthPropertyName = "Depth";

        private int _depth = 0;

        /// <summary>
        /// Sets and gets the Depth property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Depth
        {
            get
            {
                return _depth;
            }
            set
            {
                Set(DepthPropertyName, ref _depth, value);
            }
        }

        /// <summary>
        /// The <see cref="Processes" /> property's name.
        /// </summary>
        public const string ProcessesPropertyName = "Processes";

        private int _processes = 0;

        /// <summary>
        /// Sets and gets the Processes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Processes
        {
            get
            {
                return _processes;
            }
            set
            {
                Set(ProcessesPropertyName, ref _processes, value);
            }
        }

        /// <summary>
        /// The <see cref="SavePath" /> property's name.
        /// </summary>
        public const string SavePathPropertyName = "SavePath";

        private string _path = null;

        /// <summary>
        /// Sets and gets the SavePath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SavePath
        {
            get
            {
                return _path;
            }
            set
            {
                Set(SavePathPropertyName, ref _path, value);
            }
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
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择一个文件夹";
            dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SavePath = dialog.SelectedPath;
            }
        }

        private RelayCommand _defaultCommand;

        /// <summary>
        /// Gets the DefaultCommand.
        /// </summary>
        public RelayCommand DefaultCommand
        {
            get
            {
                return _defaultCommand
                    ?? (_defaultCommand = new RelayCommand(ExecuteDefaultCommand));
            }
        }

        private void ExecuteDefaultCommand()
        {
            this._kind.Clear();
            this._kind.Add(FileKind.Html, true);
            this._kind.Add(FileKind.Js, true);
            this._kind.Add(FileKind.Image, true);
            this._kind.Add(FileKind.Css, true);
            this._kind.Add(FileKind.Video, true);
            this._kind.Add(FileKind.Audio, true);
            this._kind.Add(FileKind.File, true);

            this.Url = Settings.Url;
            this.DownLoadMode = Settings.Mode;
            this.Depth = Settings.Depth;
            this.Processes = Settings.Processes;
            this.SavePath = Settings.Path;
        }

        private RelayCommand _saveCommand;

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new RelayCommand(ExecuteSaveCommand));
            }
        }

        private void ExecuteSaveCommand()
        {
            if (!Validator.IsValidUrl(Url))
            {

            }
            else
            {
                Settings.Url = Url;
                Settings.Mode = DownLoadMode;
                Settings.Kind = this._kind;
                Settings.Depth = Depth;
                Settings.Path = SavePath;
                Settings.Processes = Processes;
                _viewMessage.Execute(true);

                Messenger.Default.Unregister(this);
                ViewEnable = false;
            }
        }

        
    }
}