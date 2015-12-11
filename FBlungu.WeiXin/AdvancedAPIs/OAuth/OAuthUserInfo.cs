using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;

namespace QYSubjectWeixin.AdvancedAPIs
{
    /// <summary>
    /// 通过OAuth的获取到的用户信息（snsapi_userinfo=scope）
    /// </summary>
    public class OAuthUserInfo
    {
        public string UserId { get; set; }
    }
}
