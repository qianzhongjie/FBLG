using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBlungu.WeiXin.AdvancedAPIs.GetAccessToken
{
    public class SendMpnews : SendBase
    {
        public override string msgtype
        {
            get
            {
                return "mpnews";
            }
        }
        public Mpnews mpnews = new Mpnews();
        public class Mpnews
        {
            public string media_id { get; set; }
        }
    }
    /// <summary>
    /// 上传new和card
    /// </summary>
    public class UploadNews
    {
        /// <summary>
        /// 通用媒体上传得到media_id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
    }
    /// <summary>
    /// 上传new和card 返回码
    /// </summary>
    public class ResultNews
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 上传new或card得到media_id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int created_at { get; set; }
    }
}