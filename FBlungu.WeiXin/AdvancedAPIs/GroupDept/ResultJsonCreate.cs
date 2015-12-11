using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;

namespace QYSubjectWeixin.AdvancedAPIs.GroupDept
{
    /// <summary>
    /// 添加通讯录返回结果
    /// </summary>
    public class ResultJsonCreate : WxJsonResult
    {
        public string id { get; set; }
    }
}
