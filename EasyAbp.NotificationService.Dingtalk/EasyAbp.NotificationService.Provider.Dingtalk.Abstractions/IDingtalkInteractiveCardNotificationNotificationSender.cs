using AlibabaCloud.SDK.Dingtalkim_1_0.Models;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public interface IDingtalkInteractiveCardNotificationNotificationSender
{
    Task<SendRobotInteractiveCardResponse> SendAsync(string userId, DingtalkRobotInteractiveCardDataModel dataModel);
}