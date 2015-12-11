using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBlungu.WeiXin.AdvancedAPIs.GetAccessToken
{

    public class Articles
    {
        /// <summary>
        /// 图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得 
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 图文消息的作者 
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 图文消息的标题 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面 
        /// </summary>
        public string content_source_url { get; set; }
        /// <summary>
        ///  	图文消息页面的内容，支持HTML标签
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图文消息的描述 
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 是否显示封面，1为显示，0为不显示 
        /// </summary>
        public int show_cover_pic { get; set; }
    }
    /// <summary>
    /// 图文
    /// </summary>
    public class Mpnews
    {
        /// <summary>
        /// 图文消息，一个图文消息支持1到10条图文 
        /// </summary>
        public List<Articles> articles { get; set; }
    }

    /// <summary>
    /// 图文返回码
    /// </summary>
    public class result
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb），次数为news，即图文消息
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 媒体文件/图文消息上传后获取的唯一标识 
        /// </summary>
        public string Media_id { get; set; }
        /// <summary>
        /// 媒体文件上传时间 
        /// </summary>
        public int Created_at { get; set; }
    }
}