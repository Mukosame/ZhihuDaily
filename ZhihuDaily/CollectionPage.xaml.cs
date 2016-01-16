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
using ZhiHuDaily.UWP.Core.Models;
using ZhiHuDaily.UWP.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ZhihuDaily
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CollectionPage : Page
    {
        private CollectionViewModel _viewModel;

        public CollectionPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
                this.DataContext = _viewModel = new CollectionViewModel();
        }


        /// 点击查看收藏的文章
        /// Click to view favotite article/content
        private void listCollections_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(ContentPage), new object[] { e.ClickedItem });
        }

        object selected_item;  //选中项的数据源
        /// 右键收藏，弹出菜单

        private void Border_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            UIElement uie = e.OriginalSource as UIElement;
            selected_item = (e.OriginalSource as FrameworkElement).DataContext;
            (this.Resources["contextMenu"] as MenuFlyout).ShowAt(uie, e.GetPosition(uie));
        }

        /// 浏览收藏
        private void menuScan_Click(object sender, RoutedEventArgs e)
        {
            if (selected_item != null)
            {
                this.Frame.Navigate(typeof(ContentPage), new object[] { selected_item as Story });
            }
        }

        /// 取消收藏
        private void menuRemove_Click(object sender, RoutedEventArgs e)
        {
            if (selected_item != null)
            {
                _viewModel.ExchangeFavorite((selected_item as Story).ID);
            }
        }

    }
}
