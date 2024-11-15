using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class RobotInteractiveCardNotificationManager : NotificationManagerBase
{
    protected override string NotificationMethod =>
        NotificationProviderDingtalkConsts.RobotInteractiveCardNotificationMethod;

    private IRobotInteractiveCardNotificationDataModelJsonSerializer RobotInteractiveCardNotificationDataModelJsonSerializer =>
        LazyServiceProvider.LazyGetRequiredService<IRobotInteractiveCardNotificationDataModelJsonSerializer>();
    protected IRobotInteractiveCardNotificationNotificationSender RobotInteractiveCardNotificationNotificationSender =>
        LazyServiceProvider.LazyGetRequiredService<IRobotInteractiveCardNotificationNotificationSender>();

    protected IDingtalkUserIdProvider DingtalkUserIdProvider => LazyServiceProvider.LazyGetRequiredService<IDingtalkUserIdProvider>();


    public override async Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model)
    {
        var notificationInfo = new NotificationInfo(GuidGenerator.Create(), CurrentTenant.Id);

        
        notificationInfo.SetRobotInteractiveCardNotificationData(model.GetDataModel(RobotInteractiveCardNotificationDataModelJsonSerializer), RobotInteractiveCardNotificationDataModelJsonSerializer);

        var notifications = await CreateNotificationsAsync(notificationInfo, model);

        return (notifications, notificationInfo);
    }

    protected override async Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo)
    {
        var dataModel = notificationInfo.GetRobotInteractiveCardNotificationData(RobotInteractiveCardNotificationDataModelJsonSerializer);

        await SendTemplateMessageAsync(dataModel, notification);
    }

    [UnitOfWork]
    protected virtual async Task SendTemplateMessageAsync(RobotInteractiveCardDataModel dataModel, Notification notification)
    {
        var openId = await ResolveOpenIdAsync(dataModel.AppId, notification.UserId);

        if (openId.IsNullOrWhiteSpace())
        {
            await SetNotificationResultAsync(notification, false, NotificationProviderDingtalkConsts.UserOpenIdNotFoundFailureReason);

            return;
        }

        try
        {
            var response = await RobotInteractiveCardNotificationNotificationSender.SendAsync(openId, dataModel);

            if (response.StatusCode == 200)
            {
                await SetNotificationResultAsync(notification, true);
            }
            else
            {
                await SetNotificationResultAsync(notification, false, $"[{response.StatusCode}] {response.Body.ProcessQueryKey}");
            }
        }
        catch (Exception e)
        {
            Logger.LogException(e);
            var message = e is IHasErrorCode b ? b.Code ?? e.Message : e.ToString();
            await SetNotificationResultAsync(notification, false, message);
        }
    }

    protected virtual async Task<string> ResolveOpenIdAsync([CanBeNull] string appId, Guid userId)
    {
        if (appId.IsNullOrWhiteSpace())
        {
            return await DingtalkUserIdProvider.GetOrNullAsync(userId);
        }

        return await DingtalkUserIdProvider.GetOrNullAsync(appId!, userId);
    }
}