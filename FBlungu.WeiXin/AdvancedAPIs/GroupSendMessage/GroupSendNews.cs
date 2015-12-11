using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    public class GroupSendNews : GroupSendModelBase
    {
        public override string msgtype
        {
            get
            {
                return "news";
            }
        }
        /// <summary>
        /// 消息类型，此时固定为：news 
        /// </summary>
        public News news = new News();
        public class News
        {
            public List<Articles> articles = new List<Articles>();
        }
        public class Articles
        {
            public string title { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string picurl { get; set; }
        }
    }
}
