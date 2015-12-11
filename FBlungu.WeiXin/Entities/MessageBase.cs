using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.Entities
{
    public interface IMessageBase
    {
        string ToUserName { get; set; }
        string FromUserName { get; set; }
        DateTime CreateTime { get; set; }
        /// <summary>
        /// 应用id
        /// </summary>
        string AgentID { get; set; }       
        /// <summary>
        /// 回调随机加密字符串
        /// </summary>
        string EncodingAESKey { get; set; }
        /// <summary>
        /// 回调token
        /// </summary>
        string calltoken { get; set; }
        /// <summary>
        /// 企业id
        /// </summary>
        string Entid { get; set; }
    }

    /// <summary>
    /// 所有Request和Response消息的基类
    /// </summary>
    public class MessageBase
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 应用id
        /// </summary>
        public string AgentID { get; set; }

        /// <summary>
        /// 回调token
        /// </summary>
        public string calltoken { get; set; }
        /// <summary>
        /// 随机加密字符串
        /// </summary>
        public string EncodingAESKey { get; set; }
        /// <summary>
        /// 企业id
        /// </summary>
        public string Entid { get; set; }
    }
}
