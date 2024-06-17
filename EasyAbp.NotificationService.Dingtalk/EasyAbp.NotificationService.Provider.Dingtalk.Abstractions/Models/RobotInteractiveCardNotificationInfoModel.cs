using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class RobotInteractiveCardNotificationInfoModel : CreateNotificationInfoModel
{
    public RobotInteractiveCardNotificationInfoModel([NotNull] string notificationMethod, IEnumerable<Guid> userIds)
        :
        base(notificationMethod, userIds)
    {
        NotificationMethod = notificationMethod;
        UserIds = userIds;
    }

    public RobotInteractiveCardNotificationInfoModel([NotNull] string notificationMethod, Guid userId)
        :
        base(notificationMethod, userId)
    {
        NotificationMethod = notificationMethod;
        UserIds = new List<Guid> { userId };
    } 

    public string JsonDataModel
    {
        get => this.GetJsonDataModel();
        set => this.SetJsonDataModel(value);
    }
}