using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBlungu.WeiXin.AdvancedAPIs.GetAccessToken;

namespace FBlungu.WeiXin.AdvancedAPIs.GetAccessToken
{
    public class SendText:SendBase
    {
        public override string msgtype
        {
            get
            {
                return "text";
            }
        }
        public Text text = new Text();
        public class Text
        {
            public string Content { get; set; }
        }
    }
}