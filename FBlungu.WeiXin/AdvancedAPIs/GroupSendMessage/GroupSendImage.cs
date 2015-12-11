using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    public class GroupSendImage : GroupSendModelBase
    {
        public override string msgtype
        {
            get
            {
                return "image";
            }
        }
        /// <summary>
        /// 消息类型，此时固定为：image 
        /// </summary>
        public Image image = new Image();
        public class Image
        {
            public string media_id { get; set; }
        }
    }
}
