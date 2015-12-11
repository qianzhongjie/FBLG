using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBlungu.WeiXin.AdvancedAPIs.GetAccessToken
{
    public class SendWxcard:SendBase
    {
        public override string msgtype
        {
            get
            {
                return "wxcard";
            }
        }
        public Wxcard wxcard = new Wxcard();
        public class Wxcard
        {
            public string media_id { get; set; }
        }
    }
}