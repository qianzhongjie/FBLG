using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    public class GroupSendFile : GroupSendModelBase
    {

        /// <summary>
        /// 消息类型，此时固定为：file 
        /// </summary>
        public File file = new File();
        public class File
        {
            public string media_id { get; set; }
        }

        public override string msgtype
        {
            get
            {
                return "file";
            }
        }


    }
}
