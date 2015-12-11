using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;

namespace QYSubjectWeixin.AdvancedAPIs.GroupDept
{
    public class ResultJsonGetDept : WxJsonResult
    {
        public List<DepartMent> department = new List<DepartMent>();
        public class DepartMent
        {
            public string id { get; set; }
            public string name { get; set; }
            public string parentid { get; set; }
        }
    }
}
