using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;

namespace QYSubjectWeixin.AdvancedAPIs.GroupUsers
{
    /// <summary>
    /// 某部门下的企业成员
    /// </summary>
    public class EntiteGroupUserList : WxJsonResult
    {
        public List<UserList> userlist = new List<UserList>();
        public class UserList
        {
            public string userid { get; set; }
            public string name { get; set; }
            public int[] department { get; set; }
            public string position { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string weixinid { get; set; }
            public string avatar { get; set; }
            public string status { get; set; }
            public Attrs1 extattr { get; set; }
        }
        public class Attrs1
        {
            public List<Attrs> attrs { get; set; }
        }
        public class Attrs
        {
            public string name { get; set; }
            public string value { get; set; }
        }
    }
}
