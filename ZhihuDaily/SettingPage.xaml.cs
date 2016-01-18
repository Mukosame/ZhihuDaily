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
using ZhiHuDaily.UWP.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ZhihuDaily
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        SettingViewModel _viewModel;

        public SettingPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = _viewModel = new SettingViewModel();
        }

        /// <summary>
        /// 无图模式
        /// No Image Mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsNoImagesMode_Toggled(object sender, RoutedEventArgs e)
        {
            _viewModel.ExchangeNoImagesMode((sender as ToggleSwitch).IsOn);
        }
        /// <summary>
        /// 夜间模式
        /// Night Mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsNightMode_Toggled(object sender, RoutedEventArgs e)
        {
            _viewModel.ExchangeDarkMode((sender as ToggleSwitch).IsOn);
        }
        /// <summary>
        /// 保存到SD卡
        /// Save to SD Card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsSD_Toggled(object sender, RoutedEventArgs e)
        {
            //TODO
        }
        /// <summary>
        /// 主页磁贴透明化
        /// Transparent Stick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsStick_Toggled(object sender, RoutedEventArgs e)
        {
            //TODO
        }
        /// <summary>
        /// 自动推送更新
        /// Auto Notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsAutoNoti_Toggled(object sender, RoutedEventArgs e)
        {
            //TODO
        }
        /// <summary>
        /// 清除缓存
        /// Clear Buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clc_click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearCache();
        }

        /// <summary>
        /// 计算本地数据容量
        /// Calculate local data amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calc_click(object sender, RoutedEventArgs e)
        {            
            String storage = "";
            String localdata = "";
            String temp = "";

            UInt64 freespace = GetFreeSpace();
            storage = SizeConvertor(freespace).ToString() + " mb";
            localdata = GetLocalDataSize("dir/").ToString() + " mb";//TODO
            temp = GetLocalDataSize("dir/").ToString() + " mb";//TODO
            
            tb_storage.Text = "本地可用存储空间：" + storage;
            tb_localdata.Text = "本地保存数据：" + localdata;
            tb_temp.Text = "临时缓存文件：" + temp;
        }
        
        public async Task<UInt64> GetFreeSpace()
{//获取本地可用存储空间
    StorageFolder local = ApplicationData.Current.LocalFolder;
    var retrivedProperties = await local.Properties.RetrievePropertiesAsync(new string[] { "System.FreeSpace" });
    return (UInt64)retrivedProperties["System.FreeSpace"];
}
// usage:
UInt64 myFreeSpace = await GetFreeSpace();
        
        public float GetLocalDataSize(String folder)
        {
            long total = 0;
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                foreach (var fileName in isolatedStorage.GetFileNames(folder))
                {
                    using (var file = isolatedStorage.OpenFile(folder + fileName, FileMode.Open))
                    { total += file.Length;}//total in bytes
                }
            }
            return SizeConvertor(total);
        }
        
        public float SizeConvertor(int DataSize)
        {
            return Math.Round(DataSize/1048576, 3);
        }
        
        #region APPBARBUTTON
        private async void bclick(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9NBLGGH5G38S"));
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
