using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ZoDream.Spider.Model;
using ZoDream.Spider.Helper;

namespace ZoDream.Spider.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TestViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the TestViewModel class.
        /// </summary>
        public TestViewModel()
        {
        }


        /// <summary>
        /// The <see cref="Url" /> property's name.
        /// </summary>
        public const string UrlPropertyName = "Url";

        private string _url = "zodream.localhost";

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
        /// The <see cref="HtmlCode" /> property's name.
        /// </summary>
        public const string HtmlCodePropertyName = "HtmlCode";

        private string _htmlCode = string.Empty;

        /// <summary>
        /// Sets and gets the HtmlCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string HtmlCode
        {
            get
            {
                return _htmlCode;
            }
            set
            {
                Set(HtmlCodePropertyName, ref _htmlCode, value);
            }
        }

        /// <summary>
        /// The <see cref="ResultCode" /> property's name.
        /// </summary>
        public const string ResultCodePropertyName = "ResultCode";

        private string _resultCode = string.Empty;

        /// <summary>
        /// Sets and gets the ResultCode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ResultCode
        {
            get
            {
                return _resultCode;
            }
            set
            {
                Set(ResultCodePropertyName, ref _resultCode, value);
            }
        }

        /// <summary>
        /// The <see cref="UrlsInformation" /> property's name.
        /// </summary>
        public const string UrlsInformationPropertyName = "UrlsInformation";

        private ObservableCollection<UrlInformation> _urlsInformation = new ObservableCollection<UrlInformation>();

        /// <summary>
        /// Sets and gets the UrlsInformation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<UrlInformation> UrlsInformation
        {
            get
            {
                return _urlsInformation;
            }
            set
            {
                Set(UrlsInformationPropertyName, ref _urlsInformation, value);
            }
        }


        private RelayCommand _htmlCommand;

        /// <summary>
        /// Gets the HtmlCommand.
        /// </summary>
        public RelayCommand HtmlCommand
        {
            get
            {
                return _htmlCommand
                    ?? (_htmlCommand = new RelayCommand(ExecuteHtmlCommand));
            }
        }

        private void ExecuteHtmlCommand()
        {
            if (!string.IsNullOrWhiteSpace(Url))
            {
                Url = Helper.Url.GetComplete(Url);
                Http http = new Http(Url);
                HtmlCode = http.Response(http.Request());
            }
        }

        private RelayCommand _getCommand;

        /// <summary>
        /// Gets the GetCommand.
        /// </summary>
        public RelayCommand GetCommand
        {
            get
            {
                return _getCommand
                    ?? (_getCommand = new RelayCommand(ExecuteGetCommand));
            }
        }

        private void ExecuteGetCommand()
        {
            UrlsInformation.Clear();
            if (!string.IsNullOrWhiteSpace(HtmlCode))
            {
                foreach (UrlInformation item in Helper.Url.GetUrlFromHtml(HtmlCode))
                {
                    UrlsInformation.Add(item);
                }
            }
        }

        private RelayCommand _resultCommand;

        /// <summary>
        /// Gets the ResultCommand.
        /// </summary>
        public RelayCommand ResultCommand
        {
            get
            {
                return _resultCommand
                    ?? (_resultCommand = new RelayCommand(ExecuteResultCommand));
            }
        }

        private void ExecuteResultCommand()
        {

        }
    }
}