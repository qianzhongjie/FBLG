﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.AdvancedAPIs.GroupSendMessage
{
    public class GroupSendMpNews : GroupSendModelBase
    {
        public override string msgtype
        {
            get
            {
                return "mpnews";
            }
        }
        /// <summary>
        /// 消息类型，此时固定为：file 
        /// </summary>
        public Mpnews mpnews = new Mpnews();
        public class Mpnews
        {
            public List<Articles> articles = new List<Articles>();
            public string media_id { get; set; }                 
        }
        public class Articles
        {
            /// <summary>
            /// 图文消息的标题 
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 图文消息缩略图的media_id, 可以在上传多媒体文件接口中获得。此处thumb_media_id即上传接口返回的media_id 
            /// </summary>
            public string thumb_media_id { get; set; }
            /// <summary>
            /// 图文消息的作者 
            /// </summary>
            public string author { get; set; }
            /// <summary>
            /// 图文消息点击“阅读原文”之后的页面链接 
            /// </summary>
            public string content_source_url { get; set; }
            /// <summary>
            /// 图文消息的内容，支持html标签 
            /// </summary>
            public string content { get; set; }
            /// <summary>
            /// 图文消息的描述 
            /// </summary>
            public string digest { get; set; }
            /// <summary>
            /// 是否显示封面，1为显示，0为不显示 
            /// </summary>
            public string show_cover_pic { get; set; }           
        }
    }
}
