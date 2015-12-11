﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.Entities
{
    /// <summary>
    /// 具有EventKey属性的RequestMessage接口
    /// </summary>
    public interface IRequestMessageEventKey
    {
        string EventKey { get; set; }
    }
}
