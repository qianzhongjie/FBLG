using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBlungu.WeiXin.AdvancedAPIs.GetAccessToken
{
    public class SendVoice : SendBase
    {
        public override string msgtype
        {
            get
            {
                return "voice";
            }
        }
        public Voice voice = new Voice();
        public class Voice
        {
            public string media_id { get; set; }
        }
    }
}