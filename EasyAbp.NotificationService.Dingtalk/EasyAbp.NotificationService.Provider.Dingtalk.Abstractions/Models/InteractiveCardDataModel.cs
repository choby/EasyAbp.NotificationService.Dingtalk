using AlibabaCloud.SDK.Dingtalkcard_1_0.Models;
using AlibabaCloud.SDK.Dingtalkim_1_0.Models;
using PrivateDataValue = AlibabaCloud.SDK.Dingtalkcard_1_0.Models.PrivateDataValue;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class InteractiveCardDataModel
{
    public string? AppId { get; set; }

    /// <summary>
    /// 互动卡片的消息模板ID,必填
    /// </summary>
    public string CardTemplateId { get; set; }

    /// <summary>
    /// 基于群模板创建的群
    /// <remarks>企业内部应用，调用创建群接口获取open_conversation_id参数值。<br/>第三方企业应用，调用创建群接口获取open_conversation_id参数值。</remarks>
    /// </summary>
    public string OpenConversationId { get; set; }

    /// <summary>
    /// 接收人userId列表
    /// <remarks>
    /// receiverUserIdList参数填写分为以下情况：<br/>
    /// <b>单聊</b>：<br/>
    ///  receiverUserIdList填写用户ID，最大值20。<br/>
    /// <b>群聊：</b><br/>
    ///  receiverUserIdList填写用户ID，表示当前对应ID的群内用户可见。<br/>
    ///  receiverUserIdList参数不填写，表示当前群内所有用户可见。<br/>
    ///  <b>对应privateData、userIdType字段关于用户ID的值填写方式。</b><br/>
    /// userId模式：key填写用户userId。<br/>
    /// unionId模式：key填写用户unionId。<br/>
    /// </remarks>
    /// </summary>
    public List<string>? ReceiverUserIdList { get; set; }

    /// <summary>
    /// 唯一标示卡片的外部编码。必填
    /// </summary>
    public string OutTrackId { get; set; }

    /// <summary>
    /// 发送的会话类型
    /// </summary>
    public ConversationType ConversationType { get; set; }

    /// <summary>
    /// 卡片回调时的路由Key，用于查询注册的callbackUrl。
    /// </summary>
    public string CallbackRouteKey { get; set; }

    /// <summary>
    /// 卡片公有数据。
    /// </summary>
    public CreateAndDeliverRequest.CreateAndDeliverRequestCardData CardData { get; set; }

    /// <summary>
    /// 卡片私有数据。key：用户userId。value：用户数据。
    ///卡片公有数据。
    /// </summary>
    public Dictionary<string, PrivateDataValue> PrivateData { get; set; }

    /// <summary>
    /// 企业机器人ID，填写企业内部开发-机器人的AppKey。
    /// <see  href="https://img.alicdn.com/imgextra/i4/O1CN01YhKhZj1EYIXWvtfx0_!!6000000000363-2-tps-1904-416.png">图片说明</see>
    /// </summary>
    public string ChatBotId { get; set; }

    /// <summary>
    /// 用户ID类型：
    ///1（默认）：userid模式
    ///2：unionId模式
    /// </summary>
    public UserIdType? UserIdType { get; set; }

    /// <summary>
    /// 消息@人。格式：{"key":"value"}。
    /// key：用户ID，根据userIdType设置。
    /// value：用户名。
    /// 例如：{123456:"钉三多"}
    /// </summary>
    public Dictionary<string, string>? AtOpenIds { get; set; }

    /// <summary>
    /// 卡片操作。
    /// </summary>
    public SendInteractiveCardRequest.SendInteractiveCardRequestCardOptions CardOptions { get; set; }

    /// <summary>
    /// 是否开启卡片纯拉模式。
    ///true：开启卡片纯拉模式
    ///false：不开启卡片纯拉模式
    /// </summary>
    public bool? PullStrategy { get; set; }

    /// <summary>
    /// 文档Key
    /// </summary>
    public string DocKey { get; set; }

    /// <summary>
    /// 吊顶过期时间戳
    /// </summary>
    public long? TopExpiredTimeMillis { get; set; }

    /// <summary>
    /// 回调类型
    /// </summary>
    public CallbackType? CallbackType { get; set; }
}

public enum ConversationType
{
    /// <summary>
    /// 酷应用单聊
    /// </summary>
    Single = 0,

    ///  <summary>
    /// 群聊
    /// </summary>
    Group = 1,

    /// <summary>
    /// 吊顶
    /// </summary>
    Top = 2,

    // /// <summary>
    // /// 协作
    // /// </summary>
    // CoFeed = 3,
    /// <summary>
    /// 文档
    /// </summary>
    Doc = 4,

    /// <summary>
    /// 机机器人单聊
    /// </summary>
    Robot = 5
}

public enum UserIdType
{
    /// <summary>
    /// 用户ID
    /// </summary>
    UserId = 1,

    /// <summary>
    /// 用户unionId
    /// </summary>
    UnionId = 2
}

public enum CallbackType
{
    HTTP = 0,
    STREAM = 1
}

