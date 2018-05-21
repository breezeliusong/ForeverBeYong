using ForeverBeYoung.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http;

namespace ForeverBeYoung.ViewModels
{
    public class WeiBoAPIAccessHelper
    {

        public async Task<UserInfoObject> GetUserInfoAsync(OAuthModel OAuth)
        {
            HttpClient httpClient = new HttpClient();
            string userUrl = "https://api.weibo.com/2/users/show.json" + "?access_token=" + OAuth.access_token + "&uid=" + OAuth.uid;
            Uri userUri = new Uri(userUrl);
            HttpResponseMessage responseMessage = await httpClient.GetAsync(userUri);
            StorageFile userInfoFile;
            string userInfo = string.Empty;
            if (responseMessage.IsSuccessStatusCode)
            {
                userInfo = await responseMessage.Content.ReadAsStringAsync();
                userInfoFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("UserInfo", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(userInfoFile, userInfo);
               
            }
            else
            {
                userInfoFile = await ApplicationData.Current.LocalFolder.GetFileAsync("UserInfo");
                if (userInfoFile != null)
                {
                     userInfo = await FileIO.ReadTextAsync(userInfoFile);
                }

            }
            UserInfoObject root = JsonConvert.DeserializeObject<UserInfoObject>(userInfo);
            return root;
        }


        //https://api.weibo.com/2/users/counts.json
        public async Task<UserStatuses> GetUserStatuses(OAuthModel OAuth)
        {
            HttpClient httpClient = new HttpClient();
            string userUrl = "https://api.weibo.com/2/statuses/user_timeline.json" + "?access_token=" + OAuth.access_token;
            Uri userUri = new Uri(userUrl);
            HttpResponseMessage responseMessage = await httpClient.GetAsync(userUri);
            StorageFile userStatus;
            string userInfo;
            if (responseMessage.IsSuccessStatusCode)
            {
                userInfo = await responseMessage.Content.ReadAsStringAsync();
                userStatus=await ApplicationData.Current.LocalFolder.CreateFileAsync("UserStatus", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(userStatus, userInfo);
               
            }
            else
            {
                userStatus=await ApplicationData.Current.LocalFolder.GetFileAsync("UserStatus");
                userInfo = await FileIO.ReadTextAsync(userStatus);
            }
            UserStatuses root = JsonConvert.DeserializeObject<UserStatuses>(userInfo);
            return root;
        }
    }
}
