using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public static class NotificationInfoExtensions
{
    public static void SetRobotInteractiveCardNotificationData(this NotificationInfo notificationInfo,
        [NotNull] RobotInteractiveCardDataModel dataModel,
        IRobotInteractiveCardNotificationDataModelJsonSerializer jsonSerializer)
    {
        notificationInfo.SetDataValue(NotificationProviderDingtalkConsts.NotificationInfoDataModelPropertyName,
            jsonSerializer.Serialize(Check.NotNull(dataModel, nameof(dataModel))));
    }
    
    

    public static RobotInteractiveCardDataModel GetRobotInteractiveCardNotificationData(
        this NotificationInfo notificationInfo, IRobotInteractiveCardNotificationDataModelJsonSerializer jsonSerializer)
    {
        return jsonSerializer.Deserialize(notificationInfo.GetDataValue(NotificationProviderDingtalkConsts.NotificationInfoDataModelPropertyName) as string);
    }
    
    public static void SetInteractiveCardNotificationData(this NotificationInfo notificationInfo,
        [NotNull] InteractiveCardDataModel dataModel,
        IInteractiveCardNotificationDataModelJsonSerializer jsonSerializer)
    {
        notificationInfo.SetDataValue(NotificationProviderDingtalkConsts.NotificationInfoDataModelPropertyName,
            jsonSerializer.Serialize(Check.NotNull(dataModel, nameof(dataModel))));
    }
    
    public static InteractiveCardDataModel GetInteractiveCardNotificationData(
        this NotificationInfo notificationInfo, IInteractiveCardNotificationDataModelJsonSerializer jsonSerializer)
    {
        return jsonSerializer.Deserialize(notificationInfo.GetDataValue(NotificationProviderDingtalkConsts.NotificationInfoDataModelPropertyName) as string);
    }
}