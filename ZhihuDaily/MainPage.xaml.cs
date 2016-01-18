using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZhiHuDaily.UWP.Core.Models;
using ZhiHuDaily.UWP.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ZhihuDaily
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HomeViewModel _viewModel;
        ItemsStackPanel _itemsPanel;
        ScrollViewer _scrollView;

        public MainPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            SystemNavigationManager.GetForCurrentView().BackRequested += BackPressed;
        }

        private void BackPressed(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled && Frame.CurrentSourcePageType.FullName == "ZhihuDaily.MainPage")
                Application.Current.Exit();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                this.DataContext = _viewModel = new HomeViewModel();
            }
        }

        public void OnCreate()
        {            
        }

        public void initImageLoader ()
        {
            //File cachedir = 
        }

        #region CLICK_ARTICLE_LIST
        //Click Article List
        private void listStories_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(ContentPage), new object[] { e.ClickedItem as Story });
        }
        #endregion

        #region APPBARBUTTON
        private async void bclick(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9NBLGGH5G38S"));
        }

        private void cclick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InfoPage));
        }

        private void Setting(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }

        private void Collection(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CollectionPage));
        }

        public void RefreshPage()
        {
            _viewModel.Update();
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Do something when page loaded
            List<ScrollViewer> list = new List<ScrollViewer>();
            HomeHelper.FindChildren<ScrollViewer>(list, listStories);  //先找到ScrollViewer 注册ViewChanged事件
            if (list.Count > 0)
            {
                _scrollView = list[0];
                _scrollView.ViewChanged += HomePage_ViewChanged;
            }
            List<ItemsStackPanel> list2 = new List<ItemsStackPanel>();
            HomeHelper.FindChildren<ItemsStackPanel>(list2, listStories);  //找到ItemStackPanel 它包含FirstVisibleIndex属性
            if (list.Count > 0)
            {
                _itemsPanel = list2[0];
            }
        }
        //Scroll List
        private void HomePage_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (_scrollView.VerticalOffset > 220)
            {
                if (_itemsPanel != null)
                {
                    _viewModel.Title = HomeHelper.DateStringFormat((listStories.Items[_itemsPanel.FirstVisibleIndex] as Story).Date);
                }
            }
            else
            {
                _viewModel.Title = "首页";
            }
        }



    }

    class HomeHelper
    {
        internal static void FindChildren<T>(List<T> results, DependencyObject startNode)
                where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }

        internal static string DateStringFormat(string date)
        {
            string date_new = date.Insert(4, "/").Insert(7, "/");
            DateTime t = DateTime.Parse(date_new);
            if (t.Date.Equals(DateTime.Now.Date))
            {
                return "今日热闻";
            }
            else
            {
                return t.Date.ToString("MM月dd日 dddd", new System.Globalization.CultureInfo("zh-CN"));
            }
        }
    }
        
        /// 每项背景色 转换 
        class ItemBackgroundFormat : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                if (DataShareManager.Current.APPTheme == ElementTheme.Light)
                {
                    if ((bool)value)
                    {
                        return new SolidColorBrush(Colors.Gray);
                    }
                    else
                    {
                        return new SolidColorBrush(Colors.Black);
                    }
                }
                else
                {
                    if ((bool)value)
                    {
                        return new SolidColorBrush(Colors.Gray);
                    }
                    else
                    {
                        return new SolidColorBrush(Colors.White);
                    }
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                throw new NotImplementedException();
            }
        }
    
}
