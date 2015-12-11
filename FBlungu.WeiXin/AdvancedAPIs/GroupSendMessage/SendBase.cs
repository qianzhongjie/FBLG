using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace FBlungu.WeiXin.AdvancedAPIs.GetAccessToken
{
    public interface ISendBase
    {
        Filter filter { get; set; }
        string msgtype { get; set; }
    }
    [DataContract]
    public class SendBase : ISendBase
    {
        [DataMember]
        /// <summary>
        /// 用于设定消息的接收者
        /// </summary>
        public Filter filter { get; set; }
        [DataMember]
        /// <summary>
        /// 群发的消息类型，图文消息为mpnews，文本消息为text，语音为voice，音乐为music，图片为image，视频为video，卡券为wxcard 
        /// </summary>
        public virtual string msgtype { get; set; }
    }
    [DataContract]
    /// <summary>
    /// 消息接收者
    /// </summary>
    public class Filter
    {
        [DataMember]
        /// <summary>
        /// 用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户 
        /// </summary>
        public string is_to_all { get; set; }
        [DataMember]
        /// <summary>
        /// 群发到的分组的group_id，参加用户管理中用户分组接口，若is_to_all值为true，可不填写group_id 
        /// </summary>
        public string group_id { get; set; }
    }
    [DataContract]
    /// <summary>
    /// 返回码
    /// </summary>
    public class ResrultMsg
    {
        [DataMember]
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb），图文消息为news
        /// </summary>
        public string type { get; set; }
        [DataMember]
        /// <summary>
        /// 错误码 
        /// </summary>
        public string errcode { get; set; }
        [DataMember]
        /// <summary>
        /// 错误信息 
        /// </summary>
        public string errmsg { get; set; }
        [DataMember]
        /// <summary>
        /// 消息ID 
        /// </summary>
        public string msg_id { get; set; }
    }
}