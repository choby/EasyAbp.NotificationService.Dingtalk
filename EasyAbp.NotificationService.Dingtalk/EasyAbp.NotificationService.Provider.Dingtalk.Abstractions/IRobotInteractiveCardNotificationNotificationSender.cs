using AlibabaCloud.SDK.Dingtalkim_1_0.Models;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public interface IRobotInteractiveCardNotificationNotificationSender
{
    Task<SendRobotInteractiveCardResponse> SendAsync(string userId, RobotInteractiveCardDataModel dataModel);
}