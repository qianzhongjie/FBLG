using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    public interface IGroupSendModeBase
    {
        string touser { get; set; }
        string toparty { get; set; }
        string totag { get; set; }
        string agentid { get; set; }
        string safe { get; set; }
        string msgtype { get; set; }
    }
    // [Serializable]
    [DataContract]
    public class GroupSendModelBase : IGroupSendModeBase
    {
        /// <summary>
        /// UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送 
        /// </summary>
        [DataMember]
        public string touser { get; set; }
        /// <summary>
        /// PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数 
        /// </summary>
        [DataMember]
        public string toparty { get; set; }
        /// <summary>
        ///TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数
        /// </summary>
        [DataMember]
        public string totag { get; set; }
        /// <summary>
        /// 企业应用的id，可在应用的设置页面查看 
        /// </summary>
        [DataMember]
        public string agentid { get; set; }
        /// <summary>
        /// 表示是否是保密消息，0表示否，1表示是，默认0 
        /// </summary>
        [DataMember]
        public string safe { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        public virtual string msgtype
        {
            get;
            set;
        }
        /// <summary>
        /// 文件地址
        /// </summary>
        [DataMember]
        public string furl;
        /// <summary>
        /// 上传时间，没上传则为null
        /// </summary>
        public string utime;
    }
}
