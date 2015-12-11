using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.Entities;
using QYSubjectWeixin.CommonAPIs;

namespace QYSubjectWeixin.AdvancedAPIs.GroupUsers
{
    /// <summary>
    /// 通讯录人员管理
    /// </summary>
    public class GroupUsersManager
    {
        /// <summary>
        /// 添加通讯录成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="entiteGroupUser"></param>
        /// <returns></returns>
        public static WxJsonResult CreateGroupUsers(string accessToken, EntiteGroupUser entiteGroupUser)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}";
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, entiteGroupUser);
        }
        /// <summary>
        /// 更新通讯录成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="entiteGroupUser"></param>
        /// <returns></returns>
        public static WxJsonResult UpdateGroupUsers(string accessToken, EntiteGroupUser entiteGroupUser)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}";
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, entiteGroupUser);
        }
        /// <summary>
        /// 删除通讯录成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="uid">企业员工uid</param>
        /// <returns></returns>
        public static WxJsonResult DelGroupUses(string accessToken, string uid)
        {

            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid=" + uid;
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, null);
        }
        /// <summary>
        /// 根据用户部门获取人员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="department_id">获取的部门id </param>
        /// <param name="fetch_child">1/0：是否递归获取子部门下面的成员 </param>
        /// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加 </param>
        /// <returns></returns>
        public static EntiteGroupUserList GetGroupDeptUsers(string accessToken, string department_id, string fetch_child, string status)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id=" + department_id + "&fetch_child=" + fetch_child + "&status=" + status;
            return CommonJsonSend.Send<EntiteGroupUserList>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
        /// <summary>
        /// 根据用户部门获取人员详细信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="department_id">部门id</param>
        /// <param name="fetch_child">1：递归拉取，0：部门拉取</param>
        /// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <returns></returns>
        public static EntiteGroupUserList GetGroupDeptUsersAll(string accessToken, string department_id, string fetch_child = "1", string status = "0")
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={0}&department_id=" + department_id + "&fetch_child=" + fetch_child + "&status=" + status;
            return CommonJsonSend.Send<EntiteGroupUserList>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
        /// <summary>
        /// 根据Uid获取基本数据
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static GroupUserModels GetGroupDeptUser(string accessToken, string userid)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&&userid=" + userid;
            return CommonJsonSend.Send<GroupUserModels>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
    }
}
