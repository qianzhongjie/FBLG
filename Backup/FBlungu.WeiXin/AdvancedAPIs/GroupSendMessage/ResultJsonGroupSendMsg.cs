using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;

namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    /// <summary>
    /// 发送返回结果
    /// </summary>
    public class ResultJsonGroupSendMsg : WxJsonResult
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string msg_id { get; set; }
    }
}
