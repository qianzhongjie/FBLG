using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    public class GroupSendText : GroupSendModelBase
    {

        public override string msgtype
        {
            get
            {
                return "text";
            }
        }
        /// <summary>
        /// 消息类型，此时固定为：text 
        /// </summary>
        public Text text = new Text();
        public class Text
        {
            public string content { get; set; }
        }
    }
}
