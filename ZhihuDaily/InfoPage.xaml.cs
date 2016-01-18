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
using Windows.ApplicationModel.Email;//to send the email
using Windows.Security.ExchangeActiveSyncProvisioning; //to create email object

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ZhihuDaily
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfoPage : Page
    {
        public InfoPage()
        {
            this.InitializeComponent();
        }

        #region APPBARBUTTON
        private async void bclick(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9NBLGGH5G38S"));
        }

        private void homeclick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
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

        #region ConnectButton
        async void email(object sender, RoutedEventArgs e)
        {

            EasClientDeviceInformation CurrentDeviceInfor = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            String OSVersion = CurrentDeviceInfor.OperatingSystem;
            String Manufacturer = CurrentDeviceInfor.SystemManufacturer;
            String SystemProductName = CurrentDeviceInfor.SystemProductName;

            Windows.ApplicationModel.Email.EmailMessage mail = new Windows.ApplicationModel.Email.EmailMessage();
            mail.Subject = "[UWP-10]反馈-知乎日报";
            mail.Body = "\n\n\n生产厂商：" + Manufacturer + "\n系统名：" + SystemProductName + "\nOS版本：" + OSVersion;
            mail.To.Add(new Windows.ApplicationModel.Email.EmailRecipient("mukosame@gmail.com", "Mukosame"));
            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(mail);

        }

        //search in app store
        private async void otherapp(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(string.Format(@"ms-windows-store:search?publisher=Mukosame"));
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        #endregion
    }


}
