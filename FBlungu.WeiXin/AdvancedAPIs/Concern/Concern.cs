using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;
using QYSubjectWeixin.CommonAPIs;

namespace QYSubjectWeixin.AdvancedAPIs.Concern
{    
    /// <summary>
    /// 二次验证
    /// </summary>
    public static class Concern
    {
        /// <summary>
        /// 二次验证
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static WxJsonResult TwoVerification(string accessToken, string userId)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/authsucc?access_token={0}&userid={1}";

            return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
        }
    }
}
