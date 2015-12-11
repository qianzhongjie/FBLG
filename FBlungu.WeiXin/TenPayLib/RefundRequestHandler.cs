using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;

namespace QYSubjectWeixin.TenPayLib
{
    public class RefundRequestHandler : ClientRequestHandler
    {
        /// <summary>
        /// 退款接口
        /// </summary>
        /// <param name="httpContext"></param>
        public RefundRequestHandler(HttpContext httpContext)
            : base(httpContext)
        {
            this.SetGateUrl("https://mch.tenpay.com/refundapi/gateway/refund.xml");
        }
    }
}
