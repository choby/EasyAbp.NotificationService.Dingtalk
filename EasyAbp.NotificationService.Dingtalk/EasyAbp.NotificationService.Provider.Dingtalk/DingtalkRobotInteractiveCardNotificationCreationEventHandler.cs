using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class DingtalkRobotInteractiveCardNotificationCreationEventHandler :NotificationCreationEventHandlerBase
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    protected override string NotificationMethod => NotificationProviderDingtalkConsts.RobotInteractiveCardNotificationMethod;

    
    public DingtalkRobotInteractiveCardNotificationCreationEventHandler(IUnitOfWorkManager unitOfWorkManager, IServiceScopeFactory serviceScopeFactory)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task InternalHandleEventAsync(EntityCreatedEventData<Notification> eventData)
    {
        _unitOfWorkManager.Current.OnCompleted(async () =>
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var backgroundJobManager = scope.ServiceProvider.GetRequiredService<IBackgroundJobManager>();

            await backgroundJobManager.EnqueueAsync(
                new DingtalkInteractiveCardNotificationSendingJobArgs(eventData.Entity.TenantId,
                    eventData.Entity.Id));
        });

        return Task.CompletedTask;
    }

    
}