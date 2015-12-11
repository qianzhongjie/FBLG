using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    public class GroupSendVoice : GroupSendModelBase
    {
        public override string msgtype
        {
            get
            {
                return "voice";
            }
        }
        /// <summary>
        /// 消息类型，此时固定为：voice 
        /// </summary>
        public Voice voice = new Voice();
        public class Voice
        {
            public string media_id { get; set; }
        }
    }
}
