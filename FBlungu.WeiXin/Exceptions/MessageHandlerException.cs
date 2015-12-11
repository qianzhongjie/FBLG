using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.Exceptions
{
    public class MessageHandlerException : WeixinException
    {
          public MessageHandlerException(string message)
            : base(message, null)
        {
        }

          public MessageHandlerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
