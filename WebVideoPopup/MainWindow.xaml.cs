using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebVideoPopup.Models;
using WebVideoPopup.Services;
using WebVideoPopup.Services.Interfaces;

namespace WebVideoPopup
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IWebVideoService WebVideoService { get; set; }

        private string _targetUrl;
        public string TargetUrl
        {
            get => _targetUrl;
            set => SetField(ref _targetUrl, value, nameof(TargetUrl));
        }
        public WebVideoWrapper WebVideoWrapper { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            WebVideoService = ServiceHandler.GetCorrespondingService(Assembly.GetExecutingAssembly(), App.TargetUrl);
            WebVideoWrapper = WebVideoService.Parse(App.TargetUrl);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (WebVideoService == null) return;

            switch (WebVideoService.WebVideoType)
            {
                case Types.WebVideoType.Url:
                    TargetUrl = WebVideoWrapper.TargetUrl;
                    break;
                case Types.WebVideoType.Webpage:
                    CefSharp.WebBrowserExtensions.LoadHtml(webVideoBrowser, WebVideoWrapper.HtmlCode, WebVideoWrapper.TargetUrl);
                    break;
                default:
                    break;
            }
        }
    }
}