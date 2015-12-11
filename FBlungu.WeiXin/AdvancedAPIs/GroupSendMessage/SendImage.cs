using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBlungu.WeiXin.AdvancedAPIs.GetAccessToken;
namespace FBlungu.WeiXin.AdvancedAPIs.GetAccessToken
{
    public class SendImage : SendBase
    {
        public override string msgtype
        {
            get
            {
                return "text";
            }
        }
        public Image image = new Image();
        public class Image
        {
            public string media_id { get; set; }
        }
    }
}