using ForeverBeYoung.Models;
using ForeverBeYoung.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForeverBeYoung.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotoPage : Page
    {
        public PhotoPage()
        {
            this.InitializeComponent();
        }
        bool IsBackFromPhotoDetailedPage;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter is PhotoModel)
            {
                IsBackFromPhotoDetailedPage = true;
                GdView.ItemsSource = PageModel;
                GdView.SelectedItem = e.Parameter;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsBackFromPhotoDetailedPage)
            {
                return;
            }
            var files = await FileHelper.GetFilesAsync();
            ObservableCollection<PhotoModel> collection = await FileHelper.CreatePhotoModel(files);
            GdView.ItemsSource = collection;
            PageModel = collection;
        }

        private static ObservableCollection<PhotoModel> PageModel;

        private void GdView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame frame= Window.Current.Content as Frame;
            frame.Navigate(typeof(PhotoDetailPage),e.ClickedItem);
        }
    }

    
}
