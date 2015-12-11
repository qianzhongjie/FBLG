﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QYSubjectWeixin
{
    /// <summary>
    /// 接收消息类型
    /// </summary>
    public enum RequestMsgType
    {
        Text, //文本
        Location, //地理位置
        Image, //图片
        Voice, //语音
        Video, //视频
        Link, //连接信息
        Event, //事件推送
    }

    /// <summary>
    /// 当RequestMsgType类型为Event时,Event属性的类型
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// 进入会话(似乎已从官方API中移除)
        /// </summary>
        ENTER,

        /// <summary>
        /// 地理位置(似乎已从官方API中移除)
        /// </summary>
        LOCATION,

        /// <summary>
        /// 订阅
        /// </summary>
        subscribe,

        /// <summary>
        /// 取消订阅
        /// </summary>
        unsubscribe,

        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        CLICK,

        /// <summary>
        /// 二维码扫描
        /// </summary>
        scan,

        /// <summary>
        /// URL跳转
        /// </summary>
        VIEW,

        /// <summary>
        /// 事件推送群发结果
        /// </summary>
        MASSSENDJOBFINISH,

        /// <summary>
        /// 模板信息发送完成
        /// </summary>
        TEMPLATESENDJOBFINISH
    }


    /// <summary>
    /// 发送消息类型
    /// </summary>
    public enum ResponseMsgType
    {
        Text,
        News,
        Music,
        Image,
        Voice,
        Video,
        Transfer_Customer_Service,
        //transfer_customer_service
    }

    /// <summary>
    /// 菜单按钮类型
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 点击
        /// </summary>
        click,
        /// <summary>
        /// Url
        /// </summary>
        view
    }

    /// <summary>
    /// 上传媒体文件类型
    /// </summary>
    public enum UploadMediaFileType
    {
        /// <summary>
        /// 图片: 128K,支持JPG格式
        /// </summary>
        image,
        /// <summary>
        /// 语音：256K,播放长度不超过60s,支持AMR\MP3格式
        /// </summary>
        voice,
        /// <summary>
        /// 视频：1MB,支持MP4格式
        /// </summary>
        video,
        /// <summary>
        /// 文件：10M
        /// </summary>
        file,
    }

    /// <summary>
    /// 返回码(JSON)
    /// </summary>
    public enum ReturnCode
    {
        系统繁忙 = -1,
        请求成功 = 0,
        获取access_token时Secret错误, 或者access_token无效 = 40001,
        不合法的凭证类型 = 40002,
        不合法的UserID = 40003,
        不合法的媒体文件类型 = 40004,
        不合法的文件类型 = 40005,
        不合法的文件大小 = 40006,
        不合法的媒体文件id = 40007,
        不合法的消息类型 = 40008,
        不合法的corpid = 40013,
        不合法的access_token = 40014,
        不合法的菜单类型 = 40015,
        不合法的按钮个数 = 40016,
        不合法的按钮类型 = 40017,
        不合法的按钮名字长度 = 40018,
        不合法的按钮KEY长度 = 40019,
        不合法的按钮URL长度 = 40020,
        不合法的菜单版本号 = 40021,
        不合法的子菜单级数 = 40022,
        不合法的子菜单按钮个数 = 40023,
        不合法的子菜单按钮类型 = 40024,
        不合法的子菜单按钮名字长度 = 40025,
        不合法的子菜单按钮KEY长度 = 40026,
        不合法的子菜单按钮URL长度 = 40027,
        不合法的自定义菜单使用员工 = 40028,
        不合法的oauth_code = 40029,
        不合法的UserID列表 = 40031,
        不合法的UserID列表长度 = 40032,
        不合法的请求字符, 不能包含uxxxx格式的字符 = 40033,
        不合法的参数 = 40035,
        不合法的请求格式 = 40038,
        不合法的URL长度 = 40039,
        不合法的插件token = 40040,
        不合法的插件id = 40041,
        不合法的插件会话 = 40042,
        url中包含不合法domain = 40048,
        不合法的子菜单url域名 = 40054,
        不合法的按钮url域名 = 40055,
        不合法的agentid = 40056,
        不合法的callbackurl = 40057,
        不合法的红包参数 = 40058,
        不合法的上报地理位置标志位 = 40059,
        设置上报地理位置标志位时没有设置callbackurl = 40060,
        设置应用头像失败 = 40061,
        不合法的应用模式 = 40062,
        红包参数为空 = 40063,
        管理组名字已存在 = 40064,
        不合法的管理组名字长度 = 40065,
        不合法的部门列表 = 40066,
        标题长度不合法 = 40067,
        不合法的标签ID = 40068,
        不合法的标签ID列表 = 40069,
        列表中所有标签用户ID都不合法 = 40070,
        不合法的标签名字, 标签名字已经存在 = 40071,
        不合法的标签名字长度 = 40072,
        不合法的openid = 40073,
        news消息不支持指定为高保密消息 = 40074,
        缺少access_token参数 = 41001,
        缺少corpid参数 = 41002,
        缺少refresh_token参数 = 41003,
        缺少secret参数 = 41004,
        缺少多媒体文件数据 = 41005,
        缺少media_id参数 = 41006,
        缺少子菜单数据 = 41007,
        缺少oauthcode = 41008,
        缺少UserID = 41009,
        缺少url = 41010,
        缺少agentid = 41011,
        缺少应用头像mediaid = 41012,
        缺少应用名字 = 41013,
        缺少应用描述 = 41014,
        缺少Content = 41015,
        缺少标题 = 41016,
        缺少标签ID = 41017,
        缺少标签名字 = 41018,
        access_token超时 = 42001,
        refresh_token超时 = 42002,
        oauth_code超时 = 42003,
        插件token超时 = 42004,
        需要GET请求 = 43001,
        需要POST请求 = 43002,
        需要HTTPS = 43003,
        需要接收者关注 = 43004,
        需要好友关系 = 43005,
        需要订阅 = 43006,
        需要授权 = 43007,
        需要支付授权 = 43008,
        需要员工已关注 = 43009,
        需要处于回调模式 = 43010,
        需要企业授权 = 43011,
        多媒体文件为空 = 44001,
        POST的数据包为空 = 44002,
        图文消息内容为空 = 44003,
        文本消息内容为空 = 44004,
        多媒体文件大小超过限制 = 45001,
        消息内容超过限制 = 45002,
        标题字段超过限制 = 45003,
        描述字段超过限制 = 45004,
        链接字段超过限制 = 45005,
        图片链接字段超过限制 = 45006,
        语音播放时间超过限制 = 45007,
        图文消息超过限制 = 45008,
        接口调用超过限制 = 45009,
        创建菜单个数超过限制 = 45010,
        回复时间超过限制 = 45015,
        系统分组, 不允许修改 = 45016,
        分组名字过长 = 45017,
        分组数量超过上限 = 45018,
        账号数量超过上限 = 45024,
        不存在媒体数据 = 46001,
        不存在的菜单版本 = 46002,
        不存在的菜单数据 = 46003,
        不存在的员工 = 46004,
        解析JSONXML内容错误 = 47001,
        Api禁用 = 48002,
        redirect_uri未授权 = 50001,
        员工不在权限范围 = 50002,
        应用已停用 = 50003,
        员工状态不正确未关注状态 = 50004,
        企业已禁用 = 50005,
        部门长度不符合限制 = 60001,
        部门层级深度超过限制 = 60002,
        部门不存在 = 60003,
        父亲部门不存在 = 60004,
        不允许删除有成员的部门 = 60005,
        不允许删除有子部门的部门 = 60006,
        不允许删除根部门 = 60007,
        部门名称已存在 = 60008,
        部门名称含有非法字符 = 60009,
        部门存在循环关系 = 60010,
        管理员权限不足, userdepartmentagent无权限 = 60011,
        不允许删除默认应用 = 60012,
        不允许关闭应用 = 60013,
        不允许开启应用 = 60014,
        不允许修改默认应用可见范围 = 60015,
        不允许删除存在成员的标签 = 60016,
        不允许设置企业 = 60017,
        UserID已存在 = 60102,
        手机号码不合法 = 60103,
        手机号码已存在 = 60104,
        邮箱不合法 = 60105,
        邮箱已存在 = 60106,
        微信号不合法 = 60107,
        微信号已存在 = 60108,
        QQ号已存在 = 60109,
        部门个数超出限制 = 60110,
        UserID不存在 = 60111,
        成员姓名不合法 = 60112,
        身份认证信息微信号手机邮箱不能同时为空 = 60113,
        性别不合法 = 60114


    }

    ///// <summary>
    ///// 群发消息返回状态
    ///// </summary>
    //public enum GroupMessageStatus
    //{
    //    //高级群发消息的状态
    //    涉嫌广告 = 10001,
    //    涉嫌政治 = 20001,
    //    涉嫌社会 = 20004,
    //    涉嫌色情 = 20002,
    //    涉嫌违法犯罪 = 20006,
    //    涉嫌欺诈 = 20008,
    //    涉嫌版权 = 20013,
    //    涉嫌互推 = 22000,
    //    涉嫌其他 = 21000
    //}


    /// <summary>
    /// 语言
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        zh_CN,
        /// <summary>
        /// 中文繁体
        /// </summary>
        zh_TW,
        /// <summary>
        /// 英文
        /// </summary>
        en
    }
}
