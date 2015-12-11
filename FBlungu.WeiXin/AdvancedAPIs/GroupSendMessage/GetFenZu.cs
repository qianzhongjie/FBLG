using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBlungu.WeiXin.AdvancedAPIs.GetFenZu
{
    /// <summary>
    /// 
    /// </summary>
    public class Groups
    {
        /// <summary>
        /// 分组id，由微信分配 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 分组名字，UTF8编码 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分组内用户数量 
        /// </summary>
        public int Count { get; set; }
    }
    /// <summary>
    /// 获取分组返回数据
    /// </summary>
    public class GetFenZu
    {
        /// <summary>
        /// 公众平台分组信息列表 
        /// </summary>
        public List<Groups> groups { get; set; }
    }
}