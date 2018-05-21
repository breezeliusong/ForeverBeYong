using ForeverBeYoung.Views;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ForeverBeYoung
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavgtView.MenuItems)
            {
                if (item is NavigationViewItem && item.Content.ToString() == "Home")
                {
                    NavgtView.SelectedItem = item;
                    break;
                }
            }
        }

        private void NavgtView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string item = args.InvokedItem.ToString();
            switch (item)
            {
                case "Home":
                    ContentFrame.Navigate(typeof(HomePage));
                    break;
                case "Photo":
                    ContentFrame.Navigate(typeof(PhotoPage));
                    break;
                case "Music":
                    ContentFrame.Navigate(typeof(MusicPage));
                    break;
                case "Video":
                    ContentFrame.Navigate(typeof(VideoPage));
                    break;
            }
        }

        private void NavgtView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            switch (item.Content)
            {
                case "Home":
                    ContentFrame.Navigate(typeof(HomePage));
                    break;
                case "Photo":
                    ContentFrame.Navigate(typeof(PhotoPage));
                    break;
                case "Music":
                    ContentFrame.Navigate(typeof(MusicPage));
                    break;
                case "Video":
                    ContentFrame.Navigate(typeof(VideoPage));
                    break;
            }

        }

    }
}
