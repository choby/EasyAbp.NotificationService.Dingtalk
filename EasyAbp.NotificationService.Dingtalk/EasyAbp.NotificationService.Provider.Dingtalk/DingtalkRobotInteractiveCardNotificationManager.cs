using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class DingtalkRobotInteractiveCardNotificationManager : NotificationManagerBase
{
    protected override string NotificationMethod =>
        NotificationProviderDingtalkConsts.RobotInteractiveCardNotificationMethod;

    private IDingtalkInteractiveCardNotificationDataModelJsonSerializer DingtalkInteractiveCardNotificationDataModelJsonSerializer =>
        LazyServiceProvider.LazyGetRequiredService<IDingtalkInteractiveCardNotificationDataModelJsonSerializer>();
    protected IDingtalkInteractiveCardNotificationNotificationSender DingtalkInteractiveCardNotificationNotificationSender =>
        LazyServiceProvider.LazyGetRequiredService<IDingtalkInteractiveCardNotificationNotificationSender>();

    protected IDingtalkUserIdProvider DingtalkUserIdProvider => LazyServiceProvider.LazyGetRequiredService<IDingtalkUserIdProvider>();


    public override async Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model)
    {
        var notificationInfo = new NotificationInfo(GuidGenerator.Create(), CurrentTenant.Id);

        
        notificationInfo.SetDingtalkRobotInteractiveCardNotificationData(model.GetDataModel(DingtalkInteractiveCardNotificationDataModelJsonSerializer), DingtalkInteractiveCardNotificationDataModelJsonSerializer);

        var notifications = await CreateNotificationsAsync(notificationInfo, model.UserIds);

        return (notifications, notificationInfo);
    }

    protected override async Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo)
    {
        var dataModel = notificationInfo.GetDingtalkRobotInteractiveCardNotificationData(DingtalkInteractiveCardNotificationDataModelJsonSerializer);

        await SendTemplateMessageAsync(dataModel, notification);
    }

    [UnitOfWork]
    protected virtual async Task SendTemplateMessageAsync(DingtalkRobotInteractiveCardDataModel dataModel, Notification notification)
    {
        var openId = await ResolveOpenIdAsync(dataModel.AppId, notification.UserId);

        if (openId.IsNullOrWhiteSpace())
        {
            await SetNotificationResultAsync(notification, false, NotificationProviderDingtalkConsts.UserOpenIdNotFoundFailureReason);

            return;
        }

        try
        {
            var response = await DingtalkInteractiveCardNotificationNotificationSender.SendAsync(openId, dataModel);

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