using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;
using QYSubjectWeixin.HttpUtility;

namespace QYSubjectWeixin.CommonAPIs
{
    [Serializable]
    //预授权码返回类
    public class JsonappToken
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string pre_auth_code { get; set; }
        public string expires { get; set; }
    }
    //永久授权码请求类
    public class DatatogetAuthcode
    {
        public int suite_id { get; set; }
        public string auth_code { get; set; }
    }
    //永久授权码返回类
    public class Authcode
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string permanent_code { get; set; }
        public AuthcorpInfo auth_corp_info { get; set; }
        public Auth_info auth_info { get; set; }
        public Auth_user_info auth_user_info { get; set; }
    }
    //授权方企业信息
    public class AuthcorpInfo
    {
        public string corpid { get; set; }
        public string corp_name { get; set; }
        public string corp_type { get; set; }
        public string corp_round_logo_url { get; set; }
        public string corp_square_logo_url { get; set; }
        public string corp_user_max { get; set; }
        public string corp_agent_max { get; set; }
        public string wxqrcode { get; set; }
    }
    public class Auth_info
    {
        public List<Agent> agent { get; set; }
        public List<Department> department { get; set; }
    }
    public class Agent
    {
        public string agentid { get; set; }
        public string name { get; set; }
        public string square_logo_url { get; set; }
        public string round_logo_url { get; set; }
        public string appid { get; set; }
        public string api_group { get; set; }
    }
    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parentid { get; set; }
        public bool writable { get; set; }
    }
    public class Auth_user_info
    {
        public string email { get; set; }
        public string mobile { get; set; }
    }
    //微信请求返回类
    public class JsonwxResult
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }
    //设置企业号应用请求类
    public class Datatoapp
    {
        public string suite_id { get; set; }
        public string auth_corpid { get; set; }
        public string permanent_code { get; set; }
        public SetAgent agent { get; set; }
    }
    public class SetAgent
    {
        public int agentid { get; set; }
        public int report_location_flag { get; set; }//企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报  
        public string logo_mediaid { get; set; }//企业应用头像的mediaid，通过多媒体接口上传图片获得mediaid，上传后会自动裁剪成方形和圆形两个头像 
        public string name { get; set; }
        public string description { get; set; }// 	企业应用详情 
        public string redirect_domain { get; set; }//企业应用可信域名 
        public int isreportuser { get; set; }//是否接收用户变更通知。0：不接收；1：接收 
        public int isreportenter { get; set; }//是否上报用户进入应用事件。0：不接收；1：接收 
    }
    //获取企业token请求类
    public class Gettoken
    {
        public string suite_id { get; set; }
        public string auth_corpid { get; set; }
        public string permanent_code { get; set; }
    }
    public class JsonResultGetsuitetoken
    {
        public string suite_id { get; set; }
        public string suite_secret { get; set; }
        public string suite_ticket { get; set; }
    }
    //获取应用suite_access_token返回类
    public class Suitetoken
    {
        public string suite_access_token { get; set; }//两小时跟新一次
        public string expires_in { get; set; }
    }
    //获取企业token返回类
    public class SuiteToken
    {
        public string access_token { get; set; }//两小时跟新一次
        public string expires_in { get; set; }
    }
    public class AppRegister
    {
        /// <summary>
        /// 获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static AccessTokenResult GetToken(string corpid, string secret)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", corpid, secret);
            AccessTokenResult result = Get.GetJson<AccessTokenResult>(url);
            return result;
        }
    }
}
