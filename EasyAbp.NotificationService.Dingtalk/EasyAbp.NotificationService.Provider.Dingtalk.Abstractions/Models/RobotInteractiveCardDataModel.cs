

using AlibabaCloud.SDK.Dingtalkim_1_0.Models;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class RobotInteractiveCardDataModel
{
    public string? AppId { get; set; }
    /// <summary>
    /// 卡片搭建平台模板ID，固定值填写为StandardCard。
    /// </summary>
    public string CardTemplateId => "StandardCard";

    /// <summary>
    /// 接收卡片的加密群ID，特指多人群会话（非单聊）。
    /// <remarks>openConversationId和singleChatReceiver 二选一必填。</remarks>
    /// </summary>
    public string OpenConversationId { get; set; }
    /// <summary>
    /// 单聊会话接收者json串。
    /// <remarks>openConversationId和singleChatReceiver 二选一必填。</remarks>
    /// </summary>
    public string SingleChatReceiver { get; set; }
    public string CardBizId { get; set; }
    public string CardData { get; set; }
    /// <summary>
    /// 可控制卡片回调的URL，不填则无需回调。
    /// </summary>
    public string CallbackUrl { get; set; }

    /// <summary>
    /// 	卡片模板userId差异用户参数，json结构体。
    /// </summary>
    public string UserIdPrivateDataMap { get; set; }
    /// <summary>
    /// 卡片模板unionId差异用户参数，json结构体。
    /// </summary>
    public string UnionIdPrivateDataMap { get; set; }

    /// <summary>
    /// 互动卡片发送选项。
    /// </summary>
    public SendRobotInteractiveCardRequest.SendRobotInteractiveCardRequestSendOptions SendOptions { get; set; }

    /// <summary>
    /// 是否开启卡片纯拉模式。

    ///true：开启卡片纯拉模式
    ///false：不开启卡片纯拉模式
    /// </summary>
    public bool? PullStrategy { get; set; }
}


