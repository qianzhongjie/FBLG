using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    public class GroupSendVideo : GroupSendModelBase
    {
        public override string msgtype
        {
            get
            {
                return "video";
            }
        }
        /// <summary>
        /// 消息类型，此时固定为：video 
        /// </summary>
        public Video video = new Video();
        public class Video
        {
            public string media_id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
        }
    }
}
