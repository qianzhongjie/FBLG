using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.CommonAPIs;
using FBlungu.WeiXin.AdvancedAPIs.GetAccessToken;

namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    /// <summary>
    /// 企业发送接口
    /// </summary>
    public class GroupSendMessageManager
    {
        /// <summary>
        /// 群发信息接口
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ResultJsonGroupSendMsg GroupSendMessage(string accessToken, ISendBase sendData)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
            ResultJsonGroupSendMsg result = null;
            switch (sendData.msgtype)
            {
                case "image":
                    result = CommonJsonSend.Send<ResultJsonGroupSendMsg>(accessToken, urlFormat, sendData as SendImage);
                    break;
                case "mpnews":
                    result = CommonJsonSend.Send<ResultJsonGroupSendMsg>(accessToken, urlFormat, sendData as SendMpnews);
                    break;
                case "text":
                    result = CommonJsonSend.Send<ResultJsonGroupSendMsg>(accessToken, urlFormat, sendData as SendText);
                    break;
                case "mpvideo":
                    result = CommonJsonSend.Send<ResultJsonGroupSendMsg>(accessToken, urlFormat, sendData as SendMpvideo);
                    break;
                case "voice":
                    result = CommonJsonSend.Send<ResultJsonGroupSendMsg>(accessToken, urlFormat, sendData as SendVoice);
                    break;
                case "wxcard":
                    result = CommonJsonSend.Send<ResultJsonGroupSendMsg>(accessToken, urlFormat, sendData as SendWxcard);
                    break;
            }
            return result;
        }

        public static ResultJsonGroupSendMsg GroupSendMessage(string accessToken, string jsonObject)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
            ResultJsonGroupSendMsg result = CommonJsonSend.Send<ResultJsonGroupSendMsg>(accessToken, urlFormat, jsonObject, CommonJsonSendType.POST, false);
            return result;
        }
    }
}
