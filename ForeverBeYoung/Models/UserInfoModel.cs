using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeverBeYoung.Models
{
    class UserInfoModel
    {

    }

    public class PicUrl
    {
        public string thumbnail_pic { get; set; }
    }

    public class Visible
    {
        public int type { get; set; }
        public int list_id { get; set; }
    }

    public class Status
    {
        public string created_at { get; set; }
        public long id { get; set; }
        public string mid { get; set; }
        public string idstr { get; set; }
        public bool can_edit { get; set; }
        public string text { get; set; }
        public int textLength { get; set; }
        public int source_allowclick { get; set; }
        public int source_type { get; set; }
        public string source { get; set; }
        public bool favorited { get; set; }
        public bool truncated { get; set; }
        public string in_reply_to_status_id { get; set; }
        public string in_reply_to_user_id { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public List<PicUrl> pic_urls { get; set; }
        public string thumbnail_pic { get; set; }
        public string bmiddle_pic { get; set; }
        public string original_pic { get; set; }
        public object geo { get; set; }
        public bool is_paid { get; set; }
        public int mblog_vip_type { get; set; }
        public int reposts_count { get; set; }
        public int comments_count { get; set; }
        public int attitudes_count { get; set; }
        public int pending_approval_count { get; set; }
        public bool isLongText { get; set; }
        public int mlevel { get; set; }
        public Visible visible { get; set; }
        public Int64 biz_feature { get; set; }
        public int hasActionTypeCard { get; set; }
        public User user { get; set; }
        public List<object> darwin_tags { get; set; }
        public List<object> hot_weibo_tags { get; set; }
        public List<object> text_tag_tips { get; set; }
        public int userType { get; set; }
        public int more_info_type { get; set; }
        public int positive_recom_flag { get; set; }
        public int content_auth { get; set; }
        public string gif_ids { get; set; }
        public int is_show_bulletin { get; set; }
        public CommentManageInfo comment_manage_info { get; set; }
    }

    public class UserInfoObject
    {
        public long id { get; set; }
        public string idstr { get; set; }
        public int @class { get; set; }
        public string screen_name { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string profile_image_url { get; set; }
        public string cover_image_phone { get; set; }
        public string profile_url { get; set; }
        public string domain { get; set; }
        public string weihao { get; set; }
        public string gender { get; set; }
        public int followers_count { get; set; }
        public int friends_count { get; set; }
        public int pagefriends_count { get; set; }
        public int statuses_count { get; set; }
        public int favourites_count { get; set; }
        public string created_at { get; set; }
        public bool following { get; set; }
        public bool allow_all_act_msg { get; set; }
        public bool geo_enabled { get; set; }
        public bool verified { get; set; }
        public int verified_type { get; set; }
        public string remark { get; set; }
        public Insecurity insecurity { get; set; }
        public Status status { get; set; }
        public int ptype { get; set; }
        public bool allow_all_comment { get; set; }
        public string avatar_large { get; set; }
        public string avatar_hd { get; set; }
        public string verified_reason { get; set; }
        public string verified_trade { get; set; }
        public string verified_reason_url { get; set; }
        public string verified_source { get; set; }
        public string verified_source_url { get; set; }
        public bool follow_me { get; set; }
        public bool like { get; set; }
        public bool like_me { get; set; }
        public int online_status { get; set; }
        public int bi_followers_count { get; set; }
        public string lang { get; set; }
        public int star { get; set; }
        public int mbtype { get; set; }
        public int mbrank { get; set; }
        public int block_word { get; set; }
        public int block_app { get; set; }
        public int credit_score { get; set; }
        public int user_ability { get; set; }
        public int urank { get; set; }
        public int story_read_state { get; set; }
        public int vclub_member { get; set; }
    }



    public class Insecurity
    {
        public bool sexual_content { get; set; }
    }

    public class User
    {
        public object id { get; set; }
        public string idstr { get; set; }
        public int @class { get; set; }
        public string screen_name { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string profile_image_url { get; set; }
        public string cover_image_phone { get; set; }
        public string profile_url { get; set; }
        public string domain { get; set; }
        public string weihao { get; set; }
        public string gender { get; set; }
        public int followers_count { get; set; }
        public int friends_count { get; set; }
        public int pagefriends_count { get; set; }
        public int statuses_count { get; set; }
        public int favourites_count { get; set; }
        public string created_at { get; set; }
        public bool following { get; set; }
        public bool allow_all_act_msg { get; set; }
        public bool geo_enabled { get; set; }
        public bool verified { get; set; }
        public int verified_type { get; set; }
        public string remark { get; set; }
        public Insecurity insecurity { get; set; }
        public int ptype { get; set; }
        public bool allow_all_comment { get; set; }
        public string avatar_large { get; set; }
        public string avatar_hd { get; set; }
        public string verified_reason { get; set; }
        public string verified_trade { get; set; }
        public string verified_reason_url { get; set; }
        public string verified_source { get; set; }
        public string verified_source_url { get; set; }
        public bool follow_me { get; set; }
        public bool like { get; set; }
        public bool like_me { get; set; }
        public int online_status { get; set; }
        public int bi_followers_count { get; set; }
        public string lang { get; set; }
        public int star { get; set; }
        public int mbtype { get; set; }
        public int mbrank { get; set; }
        public int block_word { get; set; }
        public int block_app { get; set; }
        public int credit_score { get; set; }
        public int user_ability { get; set; }
        public int urank { get; set; }
        public int story_read_state { get; set; }
        public int vclub_member { get; set; }
    }

    public class CommentManageInfo
    {
        public int comment_manage_button { get; set; }
        public int comment_permission_type { get; set; }
        public int approval_comment_type { get; set; }
    }

    public class UserStatuses
    {
        public List<Status> statuses { get; set; }
        public List<object> marks { get; set; }
        public bool hasvisible { get; set; }
        public int previous_cursor { get; set; }
        public int next_cursor { get; set; }
        public int total_number { get; set; }
        public int interval { get; set; }
    }
}
