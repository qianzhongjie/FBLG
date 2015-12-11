using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.CommonAPIs;
using QYSubjectWeixin.Entities;

namespace QYSubjectWeixin.AdvancedAPIs.GroupDept
{

    /// <summary>
    /// 通讯录管理
    /// </summary>
    public class GroupDeptManager
    {
        /// <summary>
        /// 添加通讯录
        /// </summary>
        /// <param name="accessToken">调用接口凭证 </param>
        /// <param name="deptName">部门名称。长度限制为1~64个字符 </param>
        /// <param name="parentid">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后  </param>
        /// <returns></returns>
        public static ResultJsonCreate CreateGroupDept(string accessToken, string deptName, string parentid, int order = 1)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}";
            var data = new
            {
                name = deptName,
                parentid = parentid,
                order = order
            };
            return CommonJsonSend.Send<ResultJsonCreate>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 更新通讯录
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门id</param>
        /// <param name="deptName">更新的部门名称。长度限制为1~64个字符。修改部门名称时指定该参数</param>
        /// <param name="parentid">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后 </param>
        /// <returns></returns>
        public static WxJsonResult UpdateGroupDept(string accessToken, string id, string deptName, string parentid, int order)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}";
            var data = new
            {
                id = id,
                name = deptName,
                parentid = parentid,
                order = order
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="accessToken"></param>        
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static WxJsonResult DelGroupDept(string accessToken,string moreid)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token={0}&id=" + moreid;
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, null);

        }
        /// <summary>
        /// 获取当前部门下的所有部门
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ResultJsonGetDept GetGroupDept(string accessToken)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}";
            return CommonJsonSend.Send<ResultJsonGetDept>(accessToken, urlFormat, null);
        }
    }
}
