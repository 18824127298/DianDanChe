﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigbit.App.Net.WeiXinService
{
    public enum WeiXinReturnCode
    {
        系统繁忙请稍候再试 = -1,
        请求成功 = 0,
        获取access_token时AppSecret错误或者access_token无效 = 40001,
        不合法的凭证类型 = 40002,
        不合法的OpenID = 40003,
        不合法的媒体文件类型 = 40004,
        不合法的文件类型 = 40005,
        不合法的文件大小 = 40006,
        不合法的媒体文件id = 40007,
        不合法的消息类型 = 40008,
        不合法的图片文件大小 = 40009,
        不合法的语音文件大小 = 40010,
        不合法的视频文件大小 = 40011,
        不合法的缩略图文件大小 = 40012,
        不合法的AppID = 40013,
        不合法的access_token = 40014,
        不合法的菜单类型 = 40015,
        不合法的按钮个数 = 40016,
        无合法的按钮个数2 = 40017,
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
        不合法的自定义菜单使用用户 = 40028,
        不合法的oauth_code = 40029,
        不合法的refresh_token = 40030,
        不合法的openid列表 = 40031,
        不合法的openid列表长度 = 40032,
        不合法的请求字符 = 40033,
        不合法的参数 = 40035,
        不合法的请求格式 = 40038,
        不合法的URL长度 = 40039,
        不合法的分组id = 40050,
        分组名字不合法 = 40051,
        分组名字不合法2 = 40117,
        media_id大小不合法 = 40118,
        button类型错误 = 40119,
        button类型错误2 = 40120,
        不合法的media_id类型 = 40121,
        微信号不合法 = 40132,
        不支持的图片格式 = 40137,
        缺少access_token参数 = 41001,
        缺少appid参数 = 41002,
        缺少refresh_token参数 = 41003,
        缺少secret参数 = 41004,
        缺少多媒体文件数据 = 41005,
        缺少media_id参数 = 41006,
        缺少子菜单数据 = 41007,
        缺少oauth_code = 41008,
        缺少openid = 41009,
        access_token超时 = 42001,
        refresh_token超时 = 42002,
        oauth_code超时 = 42003,
        需要GET请求 = 43001,
        需要POST请求 = 43002,
        需要HTTPS请求 = 43003,
        需要接收者关注 = 43004,
        需要好友关系 = 43005,
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
        系统分组不允许修改 = 45016,
        分组名字过长 = 45017,
        分组数量超过上限 = 45018,
        不存在媒体数据 = 46001,
        不存在的菜单版本 = 46002,
        不存在的菜单数据 = 46003,
        不存在的用户 = 46004,
        解析内容错误 = 47001,
        api功能未授权 = 48001,
        用户未授权该api = 50001,
        用户受限可能是违规后接口被封禁 = 50002,
        参数错误 = 61451,
        无效客服账号 = 61452,
        客服帐号已存在 = 61453,
        客服帐号名长度超过限制 = 61454,
        客服帐号名包含非法字符 = 61455,
        客服帐号个数超过限制 = 61456,
        无效头像文件类型 = 61457,
        系统错误 = 61450,
        日期格式错误 = 61500,
        日期范围错误 = 61501,
        POST数据参数不合法 = 9001001,
        远端服务不可用 = 9001002,
        Ticket不合法 = 9001003,
        获取摇周边用户信息失败 = 9001004,
        获取商户信息失败 = 9001005,
        获取OpenID失败 = 9001006,
        上传文件缺失 = 9001007,
        上传素材的文件类型不合法 = 9001008,
        上传素材的文件尺寸不合法 = 9001009,
        上传失败 = 9001010,
        帐号不合法 = 9001020,
        已有设备激活率低不能新增设备 = 9001021,
        设备申请数不合法必须为大于0的数字 = 9001022,
        已存在审核中的设备ID申请 = 9001023,
        一次查询设备ID数量不能超过50 = 9001024,
        设备ID不合法 = 9001025,
        页面ID不合法 = 9001026,
        页面参数不合法 = 9001027,
        一次删除页面ID数量不能超过10 = 9001028,
        页面已应用在设备中请先解除应用关系再删除 = 9001029,
        一次查询页面ID数量不能超过50 = 9001030,
        时间区间不合法 = 9001031,
        保存设备与页面的绑定关系参数错误 = 9001032,
        门店ID不合法 = 9001033,
        设备备注信息过长 = 9001034,
        设备申请参数不合法 = 9001035,
        查询起始值begin不合法 = 9001036
    }

}
