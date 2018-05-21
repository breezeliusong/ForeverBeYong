using ForeverBeYoung.Models;
using ForeverBeYoung.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForeverBeYoung.Views
{
    /// <summary>
    /// An Page to connect to Sina and get user info
    /// </summary>
    public sealed partial class HomePage : Page, INotifyPropertyChanged
    {
        public HomePage()
        {
            this.InitializeComponent();
            OAuth = OAuthModel.GetOAuthModel();
            weiBoAPIAccessHelper = new WeiBoAPIAccessHelper();
            this.Loaded += HomePage_Loaded;
        }

        private async void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            AccessToken = ApplicationData.Current.LocalSettings.Values["AccessToken"]?.ToString();
            if (AccessToken == null)
            {
                await AuthorizationHelper.GetAccessTokenAsync(OAuth);
            }
            uid = ApplicationData.Current.LocalSettings.Values["uid"]?.ToString();
            if (uid == null) return;
            Int64 _uid = Int64.Parse(uid);
            OAuth.access_token = ApplicationData.Current.LocalSettings.Values["AccessToken"]?.ToString();
            OAuth.uid = _uid;

            UserInfo = await weiBoAPIAccessHelper.GetUserInfoAsync(OAuth);
            UserStatuses= await weiBoAPIAccessHelper.GetUserStatuses(OAuth);

        }

        OAuthModel OAuth;
        WeiBoAPIAccessHelper weiBoAPIAccessHelper;
        private UserInfoObject _UserInfo;
        private UserStatuses _UserStatuses;

        private UserStatuses UserStatuses
        {
            get
            {
                return _UserStatuses;
            }
            set
            {
                _UserStatuses = value;
                RaisePropertyChanged("UserStatuses");
            }

        }


        private UserInfoObject UserInfo
        {
            get
            {
                WeiBoCount.Text = "微博数";
                return _UserInfo;
            }
            set
            {
                _UserInfo = value;
                RaisePropertyChanged("UserInfo");
            }
        }
        string AccessToken, uid;

        public event PropertyChangedEventHandler PropertyChanged;


        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
