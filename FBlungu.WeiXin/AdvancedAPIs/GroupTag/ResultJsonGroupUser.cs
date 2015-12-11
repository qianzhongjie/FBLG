using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;

namespace QYSubjectWeixin.AdvancedAPIs.GroupTag
{
    /// <summary>
    /// 增加标签成员
    /// </summary>
    public class ResultJsonGroupUser : WxJsonResult
    {
        public string invalidlist { get; set; }
    }
}
