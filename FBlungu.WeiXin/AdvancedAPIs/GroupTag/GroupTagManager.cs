using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QYSubjectWeixin.CommonAPIs;
using QYSubjectWeixin.Entities;
using QYSubjectWeixin.AdvancedAPIs.GroupUsers;

namespace QYSubjectWeixin.AdvancedAPIs.GroupTag
{
    /// <summary>
    /// 企业标签管理
    /// </summary>
    public class GroupTagManager
    {
        /// <summary>
        /// 添加企业标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagName">标签名称</param>
        /// <returns></returns>
        public static ResultJsonCreateTag CreateGropuTab(string accessToken, string tagName)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/create?access_token={0}";
            var data = new
            {
                tagname = tagName
            };
            return CommonJsonSend.Send<ResultJsonCreateTag>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 更新企业标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagName">标签名称</param>
        /// <returns></returns>
        public static WxJsonResult CreateGropuTab(string accessToken, string tagId, string tagName)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token={0}";
            var data = new
            {
                tagid = tagId,
                tagname = tagName
            };
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 删除企业标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public static WxJsonResult DelGropuTag(string accessToken, string tagId)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/delete?access_token={0}&tagid=" + tagId;
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
        /// <summary>
        /// 获取标签下面的员工
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public static EntiteGroupUserList GetGroupTagUsers(string accessToken, string tagId)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/get?access_token={0}&tagid=" + tagId;
            return CommonJsonSend.Send<EntiteGroupUserList>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 添加员工到标签下面
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ResultJsonGroupUser AddGroupTagUser(string accessToken, string tagId, string userList)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/addtagusers?access_token={0}";
            var data = new
            {
                tagid = tagId,
                userlist = userList
            };
            return CommonJsonSend.Send<ResultJsonGroupUser>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 删除员工到标签下面
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ResultJsonGroupUser DelGroupTagUser(string accessToken, string tagId, string userList)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/deltagusers?access_token={0}";
            var data = new
            {
                tagid = tagId,
                userlist = userList
            };
            return CommonJsonSend.Send<ResultJsonGroupUser>(accessToken, urlFormat, data);
        }
    }
}
