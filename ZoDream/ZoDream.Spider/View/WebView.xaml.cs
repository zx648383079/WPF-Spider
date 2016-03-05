using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using MSHTML;
using ZoDream.Spider.Model;

namespace ZoDream.Spider.View
{
    /// <summary>
    /// Description for WebView.
    /// </summary>
    public partial class WebView : Window
    {
        /// <summary>
        /// Initializes a new instance of the WebView class.
        /// </summary>
        public WebView()
        {
            InitializeComponent();
        }

        public List<HttpHeader> HttpHeaders = new List<HttpHeader>();

        private void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            _getDocument();
            
            var serviceProvider = (WebView.IServiceProvider)Browser.Document;
            if (serviceProvider != null)
            {
                Guid serviceGuid = new Guid("0002DF05-0000-0000-C000-000000000046");
                Guid iid = typeof(SHDocVw.WebBrowser).GUID;
                var webBrowserPtr = (SHDocVw.WebBrowser)serviceProvider
                    .QueryService(ref serviceGuid, ref iid);
                if (webBrowserPtr != null)
                {
                    webBrowserPtr.NewWindow2 += Browser_NewWindow2;
                }
            }
        }

        private void _refreshList()
        {
            WebList.ItemsSource = null;
            WebList.ItemsSource = HttpHeaders;
        }

        private void _getDocument()
        {
            UrlTextBox.Text = Browser.Source.ToString();
            HTMLDocument documet = Browser.Document as HTMLDocument;
            HtmlTextBox.Text = documet.getElementsByTagName("html").item(0).innerHTML;
            HttpHeaders.Clear();
            HttpHeaders.Add(new HttpHeader("Title", documet.title));
            HttpHeaders.Add(new HttpHeader("Referrer", documet.referrer));
            HttpHeaders.Add(new HttpHeader("ReadyState", documet.readyState));
            HttpHeaders.Add(new HttpHeader("Cookie", documet.cookie));
            HttpHeaders.Add(new HttpHeader("Charset", documet.charset));
            HttpHeaders.Add(new HttpHeader("Security", documet.security));
            HttpHeaders.Add(new HttpHeader("Domain", documet.domain));
            HttpHeaders.Add(new HttpHeader("FileSize", documet.fileSize));
            _refreshList();

        }

        private void Browser_NewWindow2(ref object ppDisp, ref bool cancel)
        {
            var eventArgs = new CancelEventArgs(cancel);
            _newWindow(Browser, eventArgs);
            cancel = eventArgs.Cancel;
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("6d5140c1-7436-11ce-8034-00aa006009fa")]
        internal interface IServiceProvider
        {
            [return: MarshalAs(UnmanagedType.IUnknown)]
            object QueryService(ref Guid guidService, ref Guid riid);
        }

        private void _newWindow(object sender, CancelEventArgs e)
        {
            dynamic browser = sender;
            dynamic activeElement = browser.Document.activeElement;
            var link = activeElement.ToString();
            ((WebBrowser)browser).Source = new Uri(link);
            UrlTextBox.Text = link;
            e.Cancel = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UrlTextBox.Text))
            {
                UrlTextBox.Text = Helper.Url.GetComplete(UrlTextBox.Text);
                Browser.Navigate(UrlTextBox.Text, null, null, "AcceptEncoding:gzip,deflate");
            }
        }


    }
}