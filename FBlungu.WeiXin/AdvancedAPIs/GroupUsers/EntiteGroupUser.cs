using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin.AdvancedAPIs.GroupUsers
{

    /// <summary>
    /// 添加/修改 企业员工实体
    /// </summary>
    public class EntiteGroupUser
    {
        /// <summary>
        /// 员工UserID。对应管理端的帐号，企业内必须唯一 
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员名称。长度为1~64个字符 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 成员所属部门id列表。注意，每个部门的直属员工上限为1000个 
        /// </summary>
        public int[] department { get;set;}
        /// <summary>
        /// 职位信息。长度为0~64个字符 
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 手机号码。企业内必须唯一，mobile/weixinid/email三者不能同时为空 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 性别。gender=0表示男，=1表示女。默认gender=0 
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 办公电话。长度为0~64个字符 
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 邮箱。长度为0~64个字符。企业内必须唯一 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 微信号。企业内必须唯一 
        /// </summary>
        public string weixinid { get; set; }
        /// <summary>
        /// 启用/禁用成员。1表示启用成员，0表示禁用成员 
        /// </summary>
        public string enable { get; set; }              
    }
}
