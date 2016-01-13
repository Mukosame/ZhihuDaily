using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ZhihuDaily
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContentPage : Page
    {
        public ContentPage()
        {
            this.InitializeComponent();
        }

        #region APPBARBUTTON
        private async void ShareClick(object sender, RoutedEventArgs e)
        {
            //TODO: share among other apps
            ;
        }

        private void FavoriteClick(object sender, RoutedEventArgs e)
        {
            //TODO: add this article into collection
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
}
