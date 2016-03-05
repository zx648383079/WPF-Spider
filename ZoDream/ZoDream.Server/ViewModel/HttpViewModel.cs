using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ZoDream.Server.Helper;

namespace ZoDream.Server.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class HttpViewModel : ViewModelBase
    {
        private HttpHelper _http;

        /// <summary>
        /// Initializes a new instance of the HttpViewModel class.
        /// </summary>
        public HttpViewModel()
        {
        }

        /// <summary>
        /// The <see cref="Prefix" /> property's name.
        /// </summary>
        public const string PrefixPropertyName = "Prefix";

        private string _prefix = "http://127.0.0.1:8080/";

        /// <summary>
        /// Sets and gets the Prefix property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Prefix
        {
            get
            {
                return _prefix;
            }
            set
            {
                Set(PrefixPropertyName, ref _prefix, value);
            }
        }

        /// <summary>
        /// The <see cref="HttpText" /> property's name.
        /// </summary>
        public const string HttpTextPropertyName = "HttpText";

        private string _httpText = string.Empty;

        /// <summary>
        /// Sets and gets the HttpText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string HttpText
        {
            get
            {
                return _httpText;
            }
            set
            {
                Set(HttpTextPropertyName, ref _httpText, value);
            }
        }

        /// <summary>
        /// The <see cref="HttpStatus" /> property's name.
        /// </summary>
        public const string HttpStatusPropertyName = "HttpStatus";

        private string _httpStatus = "启动";

        /// <summary>
        /// Sets and gets the HttpStatus property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string HttpStatus
        {
            get
            {
                return _httpStatus;
            }
            set
            {
                Set(HttpStatusPropertyName, ref _httpStatus, value);
            }
        }

        /// <summary>
        /// The <see cref="RequestText" /> property's name.
        /// </summary>
        public const string RequestTextPropertyName = "RequestText";

        private string _requestText = string.Empty;

        /// <summary>
        /// Sets and gets the RequestText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string RequestText
        {
            get
            {
                return _requestText;
            }
            set
            {
                Set(RequestTextPropertyName, ref _requestText, value);
            }
        }

        private RelayCommand _httpCommand;

        /// <summary>
        /// Gets the HttpCommand.
        /// </summary>
        public RelayCommand HttpCommand
        {
            get
            {
                return _httpCommand
                    ?? (_httpCommand = new RelayCommand(ExecuteHttpCommand));
            }
        }

        private void ExecuteHttpCommand()
        {
            if (null == _http)
            {
                _http = new HttpHelper(Prefix);
                _http.AddCallback(new AsyncCallback(_callback));
                HttpStatus = "停止";
            }
            else
            {
                _http.End();
                _http = null;
                HttpStatus = "启动";
            }
        }

        private void _callback(IAsyncResult result)
        {
            if (null != _http)
            {
               RequestText = _http.Request(result);
                _http.Response(HttpText);
            }
        }
    }
}