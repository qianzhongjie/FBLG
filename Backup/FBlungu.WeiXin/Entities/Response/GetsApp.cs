using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.Entities.Response
{


//   "allow_partys":{
//       "partyid": [1]
//    },
//   "allow_tags":{
//       "tagid": [1,2,3]
//    }
//   "close":0 ,
//   "redirect_domain":"www.qq.com",
//   "report_location_flag":0,
//   "isreportuser":0,
//   "isreportenter":0
//}
    public class GetsAppResult
    {   

        public int errcode { get; set; }
        public string errmsg { get; set; }
        /// <summary>
        /// 企业应用id
        /// </summary>
        public string agentid { get; set; }
        /// <summary>
        ///  	企业应用名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 企业应用方形头像 
        /// </summary>
        public string square_logo_url { get; set; }
        /// <summary>
        /// 企业应用圆形头像 
        /// </summary>
        public string round_logo_url { get; set; }
        /// <summary>
        /// 企业应用详情
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 企业应用可见范围（人员），其中包括userid和关注状态state 
        /// </summary>
        public List<Allow_userinfos> allow_userinfos { get; set; }
        /// <summary>
        ///  	企业应用可见范围（部门） 
        /// </summary>
        public List<Allow_partys> allow_partys { get; set; }
        /// <summary>
        /// 企业应用可见范围（标签） 
        /// </summary>
        public List<Allow_tags> allow_tags { get; set; }
        /// <summary>
        /// 企业应用是否被禁用
        /// </summary>
        public string close { get; set; }
        /// <summary>
        /// 企业应用可信域名 
        /// </summary>
        public string redirect_domain { get; set; }
        /// <summary>
        /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报 
        /// </summary>
        public string report_location_flag { get; set; }
        /// <summary>
        /// 是否接收用户变更通知。0：不接收；1：接收 
        /// </summary>
        public string isreportuser { get; set; }
        /// <summary>
        /// 是否上报用户进入应用事件。0：不接收；1：接收 
        /// </summary>
        public string isreportenter { get; set; }
    }

    public class Allow_userinfos
    {
        public List<User> user { get; set; }
    }
    public class User
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 关注状态
        /// </summary>
        public string status { get; set; }
    
    }
    public class Allow_partys
    {
        public string partyid { get; set; }
    }
    public class Allow_tags
    {
        public string tagid { get; set; }
    }
}
