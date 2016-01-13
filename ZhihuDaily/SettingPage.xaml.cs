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
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        #region APPBARBUTTON
        private async void bclick(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(
    new Uri(string.Format("ms-windows-store:reviewapp?appid=" + "f840285e-0a27-49a5-81d6-78edf83e82b9")));
        }

        private void cclick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InfoPage));
        }

        private void homeclick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Collection(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CollectionPage));
        }
        #endregion
    }
}
