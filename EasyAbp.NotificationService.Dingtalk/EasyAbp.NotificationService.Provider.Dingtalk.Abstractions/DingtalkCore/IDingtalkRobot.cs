using AlibabaCloud.SDK.Dingtalkim_1_0.Models;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public interface IDingtalkRobot
{
    /// <summary>
    /// 单聊机器人发送互动卡片（普通版）,https://open.dingtalk.com/document/orgapp/robots-send-interactive-cards
    /// </summary>
    /// <summary>
    /// 调试地址：https://open-dev.dingtalk.com/apiExplorer?spm=ding_open_doc.document.0.0.225e7369tLbSB3#/?devType=org&api=im_1.0%23SendRobotInteractiveCard
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="cardBizId">唯一标识一张卡片的外部ID，卡片幂等ID，可用于更新或重复发送同一卡片到多个群会话。</param>
    /// <param name="cardData">卡片模板文本内容参数，卡片json结构体。https://card.dingtalk.com/card-builder?spm=ding_open_doc.document.0.0.7a3f4a97AZtyRK</param>
    /// <param name="userId">userId和unionId二选一</param>
    /// <param name="unionId">userId和unionId二选一</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    SendRobotInteractiveCardResponse SendSingleChatInteractiveCards(string accessToken, string cardBizId, object cardData, [CanBeNull] string userId, [CanBeNull] string unionId);

}