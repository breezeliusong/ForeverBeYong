using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeverBeYoung.Models
{
    //https://api.weibo.com/oauth2/access_token?client_id=YOUR_CLIENT_ID&client_secret=YOUR_CLIENT_SECRET&grant_type=authorization_code&redirect_uri=YOUR_REGISTERED_REDIRECT_URI&code=CODE
    
    public class OAuthModel
    {
        private OAuthModel()
        {
        }

        private static OAuthModel _OAuthModel;
        


        public static OAuthModel GetOAuthModel()
        {
            if (_OAuthModel == null)
            {
                _OAuthModel = new OAuthModel();
            }
            return _OAuthModel;
        }

        string _OAuthBaseUri= "https://api.weibo.com/oauth2/authorize?";

        public string OAuthBaseUri { get { return _OAuthBaseUri; } }


        //public string OAuthBaseUri = "https://api.weibo.com/oauth2/authorize?";
        public string clientId = "1975100901";
        public string redirectUri = "http://xiaosongzi.com";
        public string code { get; set; }
        public string access_token { get; set; }
        public string remind_in { get; set; }
        public int expires_in { get; set; }
        public Int64 uid { get; set; }
        public bool isRealName { get; set; }
        public string client_secret= "cc0b40be1cc788056dfce3f771a9470e";

        public string GetTokenUri = "https://api.weibo.com/oauth2/access_token?client_id=YOUR_CLIENT_ID&client_secret=YOUR_CLIENT_SECRET&grant_type=authorization_code&redirect_uri=YOUR_REGISTERED_REDIRECT_URI&code=CODE";


    }
}
