using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Json;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class InteractiveCardNotificationManager : NotificationManagerBase
{
    protected override string NotificationMethod =>
        NotificationProviderDingtalkConsts.InteractiveCardNotificationMethod;

    private IInteractiveCardNotificationDataModelJsonSerializer InteractiveCardNotificationDataModelJsonSerializer =>
        LazyServiceProvider.LazyGetRequiredService<IInteractiveCardNotificationDataModelJsonSerializer>();

    protected IInteractiveCardNotificationNotificationSender InteractiveCardNotificationNotificationSender =>
        LazyServiceProvider.LazyGetRequiredService<IInteractiveCardNotificationNotificationSender>();

    protected IDingtalkUserIdProvider DingtalkUserIdProvider => LazyServiceProvider.LazyGetRequiredService<IDingtalkUserIdProvider>();
    protected IJsonSerializer JsonSerializer => LazyServiceProvider.LazyGetRequiredService<IJsonSerializer>();

    public override async Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model)
    {
        var notificationInfo = new NotificationInfo(GuidGenerator.Create(), CurrentTenant.Id);

        notificationInfo.SetInteractiveCardNotificationData(model.GetDataModel(InteractiveCardNotificationDataModelJsonSerializer), InteractiveCardNotificationDataModelJsonSerializer);

        var notifications = await CreateNotificationsAsync(notificationInfo, model.UserIds);//model.UserIds

        return (notifications, notificationInfo);
    }

    [UnitOfWork(true)]
    public override async Task SendNotificationsAsync(List<Notification> notifications, NotificationInfo notificationInfo, bool autoUpdateWithRepository = true)
    {
        var dataModel = notificationInfo.GetInteractiveCardNotificationData(InteractiveCardNotificationDataModelJsonSerializer);

        await SendTemplateMessageAsync(dataModel, notifications);
        if (autoUpdateWithRepository)
        {
            await NotificationRepository.UpdateManyAsync(notifications, true);
        }
    }

    protected override async Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo)
    {
        
    }

    [UnitOfWork]
    protected virtual async Task SendTemplateMessageAsync(InteractiveCardDataModel dataModel, List<Notification> notifications)
    {
        List<string> openIds = new List<string>();
        if (dataModel.ReceiverUserIdList is not null)
        {
            foreach (var userId in dataModel.ReceiverUserIdList)
            {
                var openId = await ResolveOpenIdAsync(dataModel.AppId, Guid.Parse(userId));
                openIds.Add(openId);
            }

            dataModel.ReceiverUserIdList = openIds;
        }

        Dictionary<string, string> atOpenIds = new Dictionary<string, string>();
        if (dataModel.AtOpenIds is not null)
        {
            foreach (var atOpenId in dataModel.AtOpenIds)
            {
                var openId = await ResolveOpenIdAsync(dataModel.AppId, Guid.Parse(atOpenId.Key));
                atOpenIds.Add(openId, atOpenId.Value);
            }
         
            dataModel.AtOpenIds = atOpenIds;
        }
        
        try
        {
            var response = await InteractiveCardNotificationNotificationSender.CreateAndDeliverCardsAsync(dataModel);

            if (response.StatusCode == 200)
            {
                foreach (var notification in notifications)
                {
                    await SetNotificationResultAsync(notification, true);
                }
            }
            else
            {
                foreach (var notification in notifications)
                {
                    await SetNotificationResultAsync(notification, false, $"[{response.StatusCode}] {response.Body.Result.DeliverResults[0].ErrorMsg}");
                }
            }
        }
        catch (Exception e)
        {
            Logger.LogException(e);
            var message = e is IHasErrorCode b ? b.Code ?? e.Message : e.ToString();
            foreach (var notification in notifications)
            {
                await SetNotificationResultAsync(notification, false, message);
            }
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