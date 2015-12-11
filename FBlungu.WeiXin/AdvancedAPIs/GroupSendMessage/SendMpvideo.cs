using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBlungu.WeiXin.AdvancedAPIs.GetAccessToken
{
    public class SendMpvideo : SendBase
    {
        public override string msgtype
        {
            get
            {
                return "mpvideo";
            }
        }
        public Mpvideo mpvideo = new Mpvideo();
        public class Mpvideo
        {
            public string media_id { get; set; }
        }

    }
}