using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public static class NotificationInfoExtensions
{
    public static void SetDingtalkRobotInteractiveCardNotificationData(this NotificationInfo notificationInfo,
        [NotNull] DingtalkRobotInteractiveCardDataModel dataModel,
        IDingtalkInteractiveCardNotificationDataModelJsonSerializer jsonSerializer)
    {
        notificationInfo.SetDataValue(NotificationProviderDingtalkConsts.NotificationInfoDataModelPropertyName,
            jsonSerializer.Serialize(Check.NotNull(dataModel, nameof(dataModel))));
    }

    public static DingtalkRobotInteractiveCardDataModel GetDingtalkRobotInteractiveCardNotificationData(
        this NotificationInfo notificationInfo, IDingtalkInteractiveCardNotificationDataModelJsonSerializer jsonSerializer)
    {
        return jsonSerializer.Deserialize(notificationInfo.GetDataValue(NotificationProviderDingtalkConsts.NotificationInfoDataModelPropertyName) as string);
    }
}