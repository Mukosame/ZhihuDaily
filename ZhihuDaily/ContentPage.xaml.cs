using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZhiHuDaily.UWP.Core.Models;
using ZhiHuDaily.UWP.Core.Share;
using ZhiHuDaily.UWP.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ZhihuDaily
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContentPage : Page
    {
        private StoryViewModel _viewModel;

        public ContentPage()
        {
            this.InitializeComponent();
            RegisterForShare();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void RegisterForShare()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareLinkHandler);
        }

        private void ShareLinkHandler(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = "分享文章[来自UWP应用：知乎日报]";
            request.Data.Properties.Description = "向好友分享这篇文章";
            request.Data.SetWebLink(new Uri(_viewModel.ShareUrl));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                object[] parameters = e.Parameter as object[];
                if (parameters != null)
                {
                    this.DataContext = _viewModel = new StoryViewModel(parameters[0] as Story);
                }
                imgStoryPic.Height = 200;
                HideTop();
            }
        }

        private async void HideTop()
        {
            try

            {
                await Task.Delay(1);
                imgStoryPic.Height = imgStoryPic.Height - 1;

                if (imgStoryPic.Height < 100)
                {
                    txtbStoryTitle.Visibility = Visibility.Collapsed;
                    txtbStoryImageSource.Visibility = Visibility.Collapsed;
                }
                if (imgStoryPic.Height != 0)
                {
                    HideTop();
                }
            }
            catch
            {

            }
        }



        #region APPBARBUTTON
        private void ShareClick(object sender, RoutedEventArgs e)
        {
            // share among other apps
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();;
        }

        private void FavoriteClick(object sender, RoutedEventArgs e)
        {
            //TODO: add this article into collection
            _viewModel.ExchangeFavorite();
        }

        private void Setting(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }

        private void Collection(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CollectionPage));
        }
        #endregion
    }

    class ExtensionHTMLStringProperties
    {
        // "HtmlString" attached property for a WebView
        public static readonly DependencyProperty HtmlStringProperty =
           DependencyProperty.RegisterAttached("HtmlString", typeof(string), typeof(ExtensionHTMLStringProperties), new PropertyMetadata("", OnHtmlStringChanged));

        // Getter and Setter
        public static string GetHtmlString(DependencyObject obj) { return (string)obj.GetValue(HtmlStringProperty); }
        public static void SetHtmlString(DependencyObject obj, string value) { obj.SetValue(HtmlStringProperty, value); }

        // Handler for property changes in the DataContext : set the WebView
        private static void OnHtmlStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView wv = d as WebView;
            if (wv != null)
            {
                wv.NavigateToString((string)e.NewValue);
            }
        }
    }
}
