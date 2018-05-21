using ForeverBeYoung.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace ForeverBeYoung.ViewModels
{
    public class AuthorizationHelper
    {
        public static async Task<bool> GetAccessTokenAsync(OAuthModel OAuth)
        {
            string URL = OAuth.OAuthBaseUri + "client_id=" + OAuth.clientId + "&response_type=code&redirect_uri=" + OAuth.redirectUri;
            Uri requireUri = new Uri(URL);
            Uri callbackUri = new Uri(OAuth.redirectUri);
            WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, requireUri, callbackUri);
            switch (result.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    Debug.WriteLine("success");
                    string responseData = result.ResponseData;
                    string[] Data = responseData.Split('=');
                    OAuth.code = Data[1];
                    string tokenUrl = "https://api.weibo.com/oauth2/access_token?client_id=" + OAuth.clientId + "&client_secret=" + OAuth.client_secret + "&grant_type=authorization_code&redirect_uri=" + OAuth.redirectUri + "&code=" + OAuth.code;
                    Uri tokenUri = new Uri(tokenUrl);
                    Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

                    var response = await httpClient.PostAsync(tokenUri, new HttpStringContent(""));
                    Debug.WriteLine(response.Content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseResult = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine(responseResult);

                        var splitString = responseResult.Split(new string[] { "{\"", "\",\"", "\":\"", "\":", ",\"", "\"}" }, StringSplitOptions.RemoveEmptyEntries);
                        Debug.WriteLine(splitString.ToString());
                        OAuth.access_token = splitString[1];
                        OAuth.remind_in = splitString[3];
                        OAuth.expires_in = int.Parse(splitString[5]);
                        OAuth.uid = Int64.Parse(splitString[7]);
                        OAuth.isRealName = bool.Parse(splitString[9]);
                        ApplicationData.Current.LocalSettings.Values["AccessToken"] = OAuth.access_token;
                        ApplicationData.Current.LocalSettings.Values["uid"] = OAuth.uid;
                        //{"access_token":"2.004hKTODJy_fJCc62c96775c1D7OkD","remind_in":"157679999","expires_in":157679999,"uid":"2962219841","isRealName":"true"}
                        return true;
                    }
                    else
                    {
                        await new MessageDialog("There is an error to get the Authorization").ShowAsync();
                    }
                    break;
                case WebAuthenticationStatus.ErrorHttp:
                    Debug.WriteLine("Error http");
                    await new MessageDialog("Error http").ShowAsync();
                    break;
                case WebAuthenticationStatus.UserCancel:
                    Debug.WriteLine("User cancel");
                    await new MessageDialog("User cancel").ShowAsync();
                    break;

            }
            return false;
        }
    }
}
