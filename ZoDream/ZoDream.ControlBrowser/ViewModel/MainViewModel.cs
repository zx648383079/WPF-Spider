using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ZoDream.ControlBrowser.Comparer;

namespace ZoDream.ControlBrowser.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private NotificationMessageAction<UIElement> _addAction;

        private NotificationMessageAction<UIElement> _removeAction;

        /// <summary>
        /// The <see cref="ControlsType" /> property's name.
        /// </summary>
        public const string ControlsTypePropertyName = "ControlsType";

        private List<Type> _controlsType = new List<Type>();

        /// <summary>
        /// Sets and gets the ControlsType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<Type> ControlsType
        {
            get
            {
                return _controlsType;
            }
            set
            {
                Set(ControlsTypePropertyName, ref _controlsType, value);
            }
        }

        /// <summary>
        /// The <see cref="ControlText" /> property's name.
        /// </summary>
        public const string ControlTextPropertyName = "ControlText";

        private string _controlText = string.Empty;

        /// <summary>
        /// Sets and gets the ControlText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ControlText
        {
            get
            {
                return _controlText;
            }
            set
            {
                Set(ControlTextPropertyName, ref _controlText, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Type controlType = typeof(Control);
            Assembly assembly = Assembly.GetAssembly(typeof(Control));
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(controlType) && !type.IsAbstract && type.IsPublic)
                {
                    ControlsType.Add(type);
                }
            }

            ControlsType.Sort(new TypeComparer());

            Messenger.Default.Register<NotificationMessageAction<UIElement>>(this, "add", m =>
            {
                _addAction = m;
            });

            Messenger.Default.Register<NotificationMessageAction<UIElement>>(this, "remove", m =>
            {
                _removeAction = m;
            });
        }

        private RelayCommand<Type> _selectionCommand;

        /// <summary>
        /// Gets the SelectionCommand.
        /// </summary>
        public RelayCommand<Type> SelectionCommand
        {
            get
            {
                return _selectionCommand
                    ?? (_selectionCommand = new RelayCommand<Type>(ExecuteSelectionCommand));
            }
        }

        private void ExecuteSelectionCommand(Type parameter)
        {
            try
            {;

                // Instantiate the type.
                ConstructorInfo info = parameter.GetConstructor(System.Type.EmptyTypes);
                Control control = (Control)info.Invoke(null);

                Window win = control as Window;
                if (win != null)
                {
                    // Create the window (but keep it minimized).
                    win.WindowState = System.Windows.WindowState.Minimized;
                    win.ShowInTaskbar = false;
                    win.Show();
                }
                else
                {
                    // Add it to the grid (but keep it hidden).
                    control.Visibility = Visibility.Collapsed;
                    _addAction.Execute(control);
                }

                // Get the template.
                ControlTemplate template = control.Template;

                // Get the XAML for the template.
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                StringBuilder sb = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                XamlWriter.Save(template, writer);

                // Display the template.
                ControlText = sb.ToString();

                // Remove the control from the grid.
                if (win != null)
                {
                    win.Close();
                }
                else
                {
                    _removeAction.Execute(control);
                }
            }
            catch (Exception err)
            {
                ControlText = "<< Error generating template: " + err.Message + ">>";
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}