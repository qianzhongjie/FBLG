using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using QYSubjectWeixin.Entities;
using QYSubjectWeixin.Entities.Menu;
using QYSubjectWeixin.Helpers;
using QYSubjectWeixin.HttpUtility;
using QYSubjectWeixin.Entities.Response;
using FBlungu.WeiXin.AdvancedAPIs.GetAccessToken;
using FBlungu.WeiXin.AdvancedAPIs.GetFenZu;

namespace QYSubjectWeixin.CommonAPIs
{
    /// <summary>
    /// 通用接口
    /// 通用接口用于和微信服务器通讯，一般不涉及自有网站服务器的通讯
    /// 见 http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E5%8F%A3%E6%96%87%E6%A1%A3&oldid=103
    /// </summary>
    public partial class CommonApi
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
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
                                     corpid, secret);

            AccessTokenResult result = Get.GetJson<AccessTokenResult>(url);
            return result;
        }
        /// <summary>
        /// 获取分组
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static GetFenZu GetFenZu(string access_token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}",
                                         access_token);
            GetFenZu result = Get.GetJson<GetFenZu>(url);
            return result;
        }
        /// <summary>
        /// 获取JSapi_ticket凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static JSAccessTokenResult GetJsToken(string access_token)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}",
                                     access_token);

            JSAccessTokenResult result = Get.GetJson<JSAccessTokenResult>(url);
            return result;
        }

        /// <summary>
        /// 用户信息接口(错误)
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="uid">微信员工唯一id</param>
        /// <returns></returns>
        public static WeixinUserInfoResult GetUserInfo(string accessToken, string uid)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}",
                                    accessToken, uid);
            WeixinUserInfoResult result = Get.GetJson<WeixinUserInfoResult>(url);
            return result;
        }

        /// <summary>
        /// 媒体文件上传接口
        ///注意事项
        ///1.上传的媒体文件限制：
        ///图片（image) : 1MB，支持JPG格式
        ///语音（voice）：1MB，播放长度不超过60s，支持MP4格式
        ///视频（video）：10MB，支持MP4格式
        ///缩略图（thumb)：64KB，支持JPG格式
        ///2.媒体文件在后台保存时间为3天，即3天后media_id失效
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="type">上传文件类型</param>
        /// <param name="fileName">上传文件完整路径+文件名</param>
        /// <returns></returns>
        public static UploadMediaFileResult UploadMediaFile(string accessToken, UploadMediaFileType type, string fileName)
        {
            var cookieContainer = new CookieContainer();
            var fileStream = FileHelper.GetFileStream(fileName);
            Dictionary<string, string> filekey = new Dictionary<string, string>();
            filekey.Add(Path.GetFileName(fileName), fileName);
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}&filename={2}&filelength={3}",
                accessToken, type.ToString(), Path.GetFileName(fileName), fileStream != null ? fileStream.Length : 0);
            fileStream.Close();
            fileStream.Dispose();
            UploadMediaFileResult result = Post.PostGetJson<UploadMediaFileResult>(url, cookieContainer, filekey);
            return result;
        }
        /// <summary>
        /// 媒体文件上传接口
        ///注意事项
        ///1.上传的媒体文件限制：
        ///图片（image) : 1MB，支持JPG格式
        ///语音（voice）：1MB，播放长度不超过60s，支持MP4格式
        ///视频（video）：10MB，支持MP4格式
        ///缩略图（thumb)：64KB，支持JPG格式
        ///2.媒体文件在后台保存时间为3天，即3天后media_id失效
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="type">上传文件类型</param>
        /// <param name="fileName">上传文件完整路径+文件名</param>
        /// <returns></returns>
        public static UploadMediaFileResult UploadMediaFile(string accessToken, string type, string fileName)
        {
            var cookieContainer = new CookieContainer();
            var fileStream = FileHelper.GetFileStream(fileName);
            Dictionary<string, string> filekey = new Dictionary<string, string>();
            filekey.Add(Path.GetFileName(fileName), fileName);
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}&filename={2}&filelength={3}",
                accessToken, type, Path.GetFileName(fileName), fileStream != null ? fileStream.Length : 0);
            fileStream.Close();
            fileStream.Dispose();
            UploadMediaFileResult result = Post.PostGetJson<UploadMediaFileResult>(url, cookieContainer, filekey);
            return result;
        }
        /// <summary>
        /// 上传new和Card
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="NewsOrCard"></param>
        /// <returns></returns>
        public static ResultNews UploadNewOrCard(string accessToken, UploadNews NewsOrCard)
        {
            var url = string.Format("https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token={0}", accessToken);
            ResultNews result = CommonJsonSend.Send<ResultNews>(accessToken, url, NewsOrCard, CommonJsonSendType.POST);
            return result;
        }
        /// <summary>
        /// 设置企业号应用接口
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="setup">QYSubjectWeixin.Entities.Response.SetUpApp类</param>
        /// <returns></returns>
        public static SetUpResult SetUpApp(string accessToken, SetUpApp setup)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/set?access_token={0}", accessToken);
            SetUpResult result = CommonJsonSend.Send<SetUpResult>(accessToken, url, setup, CommonJsonSendType.POST);
            return result;
        }
        /// <summary>
        /// 一键邀请用户关注接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userid">用户ID，如果是邀请部门，则为null</param>
        /// <param name="deptid">部门ID，如果是邀请用户，则为null</param>
        /// <param name="callbackurl">异步完成事件推送url</param>
        /// <param name="encodingaeskey">消息体加密方法</param>
        /// <param name="invite_tips">邀请语</param>
        /// <returns></returns>
        public static Invite Inviteattention(string accessToken, string userid, string deptid, string callbackurl, string encodingaeskey, string invite_tips = null)
        {
            SendInvite sendinvite = new SendInvite();
            sendinvite.touser = userid.Replace(',', '|');
            sendinvite.invite_tips = invite_tips;
            sendinvite.toparty = deptid.Replace(',', '|');
            sendinvite.callback.encodingaeskey = encodingaeskey;
            sendinvite.callback.token = accessToken;
            sendinvite.callback.url = callbackurl;
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/batch/inviteuser?access_token={0}");
            Invite result = CommonJsonSend.Send<Invite>(accessToken, url, sendinvite, CommonJsonSendType.POST);
            return result;
        }
        /// <summary>
        /// 获取企业号应用
        /// 
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="agentid"></param>
        /// <returns></returns>
        public static GetsAppResult GetApp(string access_token, string agentid)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/get?access_token={0}&agentid={1}", access_token, agentid);
            GetsAppResult result = CommonJsonSend.Send<GetsAppResult>(null, url, null, CommonJsonSendType.GET);
            return result;
        }



    }
}
